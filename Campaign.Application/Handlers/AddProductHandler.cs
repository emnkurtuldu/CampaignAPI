using Campaign.Application.Commands;
using Campaign.Application.Interfaces;
using Campaign.Domain.Models.Products;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Campaign.Application.Handlers
{
    public static class AddProductHandler
    {
        public class Handler : IRequestHandler<CreateProductCommand, CommonResult<object>>
        {

            private readonly IApplicationContextDB _applicationContextDB;
            private readonly IValidator<CreateProductCommand> _validators;
            private readonly ILogger<CreateProductCommand> _logger;

            public Handler(IApplicationContextDB applicationContextDB, IValidator<CreateProductCommand> validators, ILogger<CreateProductCommand> logger)
            {
                _applicationContextDB = applicationContextDB;
                _validators = validators;
                _logger = logger;
            }

            public async Task<CommonResult<object>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var validationResult = await _validators.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return CommonResult<object>.CreateError(validationResult.Errors.Select(s => s.ErrorMessage).ToArray());
                }
                _applicationContextDB.Products.Add(new Product
                {
                    ProductCode = request.ProductCode,
                    Price = request.Price,
                    Stock = request.Stock
                });
                await _applicationContextDB.SaveChangesAsync(cancellationToken);

                return CommonResult<object>.CreateResult(null);

            }
        }
    }
}
