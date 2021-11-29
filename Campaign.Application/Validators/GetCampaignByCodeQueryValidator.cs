using Campaign.Application.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Application.Validators
{
    public class GetCampaignByCodeQueryValidator : AbstractValidator<GetCampaignByCodeQuery>
    {
        public GetCampaignByCodeQueryValidator()
        {
            RuleFor(x => x.CampaignName).NotEmpty().WithMessage("Kampanya adı boş olamaz");
        }
    }
}
