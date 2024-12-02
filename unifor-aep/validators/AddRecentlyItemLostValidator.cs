using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unifor_aep.Models;

namespace unifor_aep.validators
{
    public class AddRecentlyItemLostValidator : AbstractValidator<RecentlyItemLost>
    {
        public AddRecentlyItemLostValidator()
        {
            RuleFor(m => m.id_user)
                .NotEmpty()
                    .WithMessage("O usuário não deve ser nulo!");
            RuleFor(m => m.description)
                .NotEmpty()
                    .WithMessage("A description não deve ser nulo!")
                .MaximumLength(300)
                    .WithMessage("O número de caracteres não deve ser maior que 300!");
            RuleFor(m => m.place_lost)
                .NotEmpty()
                    .WithMessage("O local não deve ser nulo!")
                .MaximumLength(100)
                    .WithMessage("O número de caracteres não deve ser maior que 100!");
            RuleFor(m => m.date)
                .NotEmpty()
                    .WithMessage("A data não deve ser nulo!");
        }
    }
}
