using Campaign.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Application.Validators
{
    public class IncreaseTimeCommandValidator : AbstractValidator<IncreaseTimeCommand>
    {
        public IncreaseTimeCommandValidator()
        {
            RuleFor(x => x.Time).GreaterThan(0).WithMessage("Saat arttırımı için sıfırdan büyük değer girilmedilir");
        }
    }
}
