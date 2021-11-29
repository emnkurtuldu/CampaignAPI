using Campaign.Application.Interfaces;
using Campaign.Application.Queries;
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
    public static class GetCampaignHandler
    {
        public class Handler : IRequestHandler<GetCampaignByCodeQuery, CommonResult<Domain.Models.Campaigns.Campaign>>
        {
            private readonly IApplicationContextDB _applicationContextDB;
            private readonly IValidator<GetCampaignByCodeQuery> _validators;
            private readonly ILogger<GetCampaignByCodeQuery> _logger;

            public Handler(IApplicationContextDB applicationContextDB, IValidator<GetCampaignByCodeQuery> validators, ILogger<GetCampaignByCodeQuery> logger)
            {
                _applicationContextDB = applicationContextDB;
                _validators = validators;
                _logger = logger;
            }

            public async Task<CommonResult<Domain.Models.Campaigns.Campaign>> Handle(GetCampaignByCodeQuery request, CancellationToken cancellationToken)
            {
                var validationResult = await _validators.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return CommonResult<Domain.Models.Campaigns.Campaign>.CreateError(validationResult.Errors.Select(s => s.ErrorMessage).ToArray());
                }
                var campaign = _applicationContextDB.Campaigns.FirstOrDefault(p => p.Name.ToLower().Contains(request.CampaignName.ToLower()));
                if (campaign != null)
                {
                    campaign.Orders = _applicationContextDB.Orders.Where(p => p.Campaign.Id == campaign.Id).ToList();
                }

                return CommonResult<Domain.Models.Campaigns.Campaign>.CreateResult(campaign);
            }
        }
    }
}
