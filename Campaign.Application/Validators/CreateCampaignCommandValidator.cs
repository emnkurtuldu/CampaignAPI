using Campaign.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Application.Validators
{
    public class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommand>
    {
        public CreateCampaignCommandValidator()
        {
            RuleFor(x => x.ProductCode).NotEmpty().WithMessage("Ürün kodu boş olamaz");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kampanya adı boş olamaz");
            RuleFor(x => x.Limit).GreaterThan(0).WithMessage("Kampanya limit bilgisi sıfır olamaz");
            RuleFor(x => x.Duration).GreaterThan(0).WithMessage("Kampanya süre bilgisi sıfır olamaz");
            RuleFor(x => x.TargetSalesCount).GreaterThan(0).WithMessage("Kampanya hedeflenen satış bilgisi sıfır olamaz");
        }
    }
}
