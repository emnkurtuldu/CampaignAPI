using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Application.Queries
{
    public class GetCampaignByCodeQuery : IRequest<CommonResult<Domain.Models.Campaigns.Campaign>>
    {
        public string CampaignName { get; }

        public GetCampaignByCodeQuery(string campaignName)
        {
            this.CampaignName = campaignName;
        }
    }

}
