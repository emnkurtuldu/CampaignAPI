using Campaign.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Application.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.ProductCode).NotEmpty().WithMessage("Ürün kodu boş olamaz");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Miktar sıfır olamaz");
        }
    }
}
