using Campaign.Application.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Application.Validators
{
    public class GetProductByCodeQueryValidator : AbstractValidator<GetProductByCodeQuery>
    {
        public GetProductByCodeQueryValidator()
        {
            RuleFor(x => x.ProductCode).NotEmpty().WithMessage("Ürün kodu boş olamaz");
        }
    }
}
