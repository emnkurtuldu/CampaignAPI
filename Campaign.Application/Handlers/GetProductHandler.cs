using Campaign.Application.Interfaces;
using Campaign.Application.Queries;
using Campaign.Domain.Models.Products;
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
    public static class GetProductHandler
    {
        public class Handler : IRequestHandler<GetProductByCodeQuery, CommonResult<Product>>
        {
            private readonly IApplicationContextDB _applicationContextDB;
            private readonly IValidator<GetProductByCodeQuery> _validators;
            private readonly ILogger<GetProductByCodeQuery> _logger;

            public Handler(IApplicationContextDB applicationContextDB, IValidator<GetProductByCodeQuery> validators, ILogger<GetProductByCodeQuery> logger)
            {
                _applicationContextDB = applicationContextDB;
                _validators = validators;
                _logger = logger;
            }

            public async Task<CommonResult<Product>> Handle(GetProductByCodeQuery request, CancellationToken cancellationToken)
            {
                var validationResult = await _validators.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return CommonResult<Product>.CreateError(validationResult.Errors.Select(s => s.ErrorMessage).ToArray());
                }
                var product = _applicationContextDB.Products.FirstOrDefault(p => p.ProductCode.ToLower().Contains(request.ProductCode.ToLower()));
                var campaign = _applicationContextDB.Campaigns.FirstOrDefault(p => p.Product.Id == product.Id);
                product.Price = CalculatorService.CalculateManipulatedPrice(campaign, product);
                    
                return CommonResult<Product>.CreateResult(product);
            }
        }
    }
}
