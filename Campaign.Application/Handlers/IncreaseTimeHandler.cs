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
    public static class IncreaseTimeHandler
    {
        public class Handler : IRequestHandler<IncreaseTimeCommand, CommonResult<object>>
        {
            private readonly IApplicationContextDB _applicationContextDB;
            private readonly IValidator<IncreaseTimeCommand> _validators;
            private readonly ILogger<IncreaseTimeCommand> _logger;
            public Handler(IApplicationContextDB applicationContextDB, IValidator<IncreaseTimeCommand> validators, ILogger<IncreaseTimeCommand> logger)
            {
                _applicationContextDB = applicationContextDB;
                _validators = validators;
                _logger = logger;
            }

            public async Task<CommonResult<object>> Handle(IncreaseTimeCommand request, CancellationToken cancellationToken)
            {
                var validationResult = await _validators.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return CommonResult<object>.CreateError(validationResult.Errors.Select(s => s.ErrorMessage).ToArray());
                }
                SystemSettings.SystemTime += TimeSpan.FromHours(request.Time);
                await SetPassiveCampaignStatus(cancellationToken, _applicationContextDB.Campaigns.Where(s => SystemSettings.SystemTime.TotalHours >= s.Duration).ToList());
                await SetPassiveCampaignStatus(cancellationToken, _applicationContextDB.Campaigns.Where(s => s.Limit <= SystemSettings.SystemTimeLinearHour).ToList());

                return CommonResult<object>.CreateResult(new { Time = SystemSettings.SystemTime.TotalHours });
            }

            private async Task SetPassiveCampaignStatus(CancellationToken cancellationToken, List<Domain.Models.Campaigns.Campaign> campaigns)
            {
                if (campaigns != null && campaigns.Count > 0)
                {
                    foreach (var item in campaigns)
                    {
                        item.IsActive = false;
                        _applicationContextDB.Campaigns.Update(item);
                    }
                    await _applicationContextDB.SaveChangesAsync(cancellationToken);
                }
            }

        }
    }
}
