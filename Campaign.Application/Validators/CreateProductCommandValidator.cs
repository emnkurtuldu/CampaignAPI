using Campaign.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Application.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.ProductCode).NotEmpty().WithMessage("Ürün kodu boş olamaz");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Ürün fiyatı sıfır olamaz");
            RuleFor(x => x.Stock).GreaterThan(0).WithMessage("Ürün miktarı sıfır olamaz");
        }
    }
}
