using Campaign.Application.Commands;
using Campaign.Application.Interfaces;
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
    public static class AddCampaignHandler
    {
        public class Handler : IRequestHandler<CreateCampaignCommand, CommonResult<object>>
        {
            private readonly IApplicationContextDB _applicationContextDB;
            private readonly IValidator<CreateCampaignCommand> _validators;
            private readonly ILogger<CreateCampaignCommand> _logger;

            public Handler(IApplicationContextDB applicationContextDB, IValidator<CreateCampaignCommand> validators, ILogger<CreateCampaignCommand> logger)
            {
                _applicationContextDB = applicationContextDB;
                _validators = validators;
                _logger = logger;
            }
            public async Task<CommonResult<object>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
            {
                var validationResult = await _validators.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    return CommonResult<object>.CreateError(validationResult.Errors.Select(s => s.ErrorMessage).ToArray());

                var relationalProduct = _applicationContextDB.Products.FirstOrDefault(s => s.ProductCode.ToLower().Contains(request.ProductCode.ToLower()));
                if (relationalProduct == null)
                    return CommonResult<object>.CreateError("Belirtilen ürün kodunda bir kayıt bulunmamaktadır. Kampanya tanımlaması yapılamaz");

                _applicationContextDB.Campaigns.Add(new Domain.Models.Campaigns.Campaign
                {
                    CreatedDate = DateTime.Now,
                    Name = request.Name,
                    Limit = request.Limit,
                    Duration = request.Duration,
                    TargetSalesCount = request.TargetSalesCount,
                    IsActive = true,
                    Product = relationalProduct

                }); 
                await _applicationContextDB.SaveChangesAsync(cancellationToken);

                return CommonResult<object>.CreateResult(null);
            }
        }
    }
}
