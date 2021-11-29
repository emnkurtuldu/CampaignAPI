using Campaign.Application.Commands;
using Campaign.Application.Interfaces;
using Campaign.Domain.Models.Orders;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Campaign.Application.Handlers
{
    public static class AddOrderHandler
    {
        public class Handler : IRequestHandler<CreateOrderCommand, CommonResult<object>>
        {
            private readonly IApplicationContextDB _applicationContextDB;
            private readonly IValidator<CreateOrderCommand> _validators;
            private readonly ILogger<CreateOrderCommand> _logger;

            public Handler(IApplicationContextDB applicationContextDB, IValidator<CreateOrderCommand> validators, ILogger<CreateOrderCommand> logger)
            {
                _applicationContextDB = applicationContextDB;
                _validators = validators;
                _logger = logger;
            }

            public async Task<CommonResult<object>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var validationResult = await _validators.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    return CommonResult<object>.CreateError(validationResult.Errors.Select(s => s.ErrorMessage).ToArray());

                var relationalProduct = _applicationContextDB.Products.FirstOrDefault(s => s.ProductCode.ToLower().Contains(request.ProductCode.ToLower()));
                if (relationalProduct == null)
                    return CommonResult<object>.CreateError("Belirtilen ürün kodunda bir kayıt bulunmamaktadır. Sipariş tanımlaması yapılamaz");
                var relationalCampaign =  _applicationContextDB.Campaigns.FirstOrDefault(s => s.Product.Id == relationalProduct.Id && s.IsActive);
                
                relationalProduct.Stock -= request.Quantity;
                _applicationContextDB.Products.Update(relationalProduct);

                var manipulatedPrice =  CalculatorService.CalculateManipulatedPrice(relationalCampaign, relationalProduct);

                _applicationContextDB.Orders.Add(new Order
                {
                    CreatedDate = DateTime.Now,
                    Quantity = request.Quantity,
                    SalesPrice = manipulatedPrice,
                    Product = relationalProduct,
                    Campaign = relationalCampaign
                    
                });
                
                await _applicationContextDB.SaveChangesAsync(cancellationToken);

                return CommonResult<object>.CreateResult(null);
            }


        }
    }
}
