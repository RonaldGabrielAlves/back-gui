using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unifor_aep.Models;

namespace unifor_aep.validators
{
    public class AddItemLostValidator : AbstractValidator<ItemLost>
    {
        public AddItemLostValidator()
        {
            RuleFor(m => m.name)
                .NotEmpty()
                    .WithMessage("O nome não deve ser nulo!")
                .MaximumLength(45)
                    .WithMessage("O número de caracteres não deve ser maior que 45!");
            RuleFor(m => m.color)
                .NotEmpty()
                    .WithMessage("A cor não deve ser nulo!")
                .MaximumLength(45)
                    .WithMessage("O número de caracteres não deve ser maior que 45!");
            RuleFor(m => m.other)
                .NotEmpty()
                    .WithMessage("O campo outro não deve ser nulo!")
                .MaximumLength(100)
                    .WithMessage("O número de caracteres não deve ser maior que 100!");
            RuleFor(m => m.id_user)
                .NotEmpty()
                    .WithMessage("O usuário não deve ser nulo!");
            RuleFor(m => m.date)
                .NotEmpty()
                    .WithMessage("A data não deve ser nulo!");
            RuleFor(m => m.image)
                .NotEmpty()
                    .WithMessage("A imagem não deve ser nulo!");
        }
    }
}

