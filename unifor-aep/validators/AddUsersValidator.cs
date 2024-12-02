using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unifor_aep.Models;

namespace unifor_aep.validators
{
    public class AddUsersValidator : AbstractValidator<User>
    {
        public AddUsersValidator()
        {
            RuleFor(m => m.name)
                .NotEmpty()
                    .WithMessage("O nome do usuário não deve ser nulo!")
                .MaximumLength(100)
                    .WithMessage("O número de caracteres não deve ser maior que 100!")
                .MinimumLength(5)
                    .WithMessage("O número de caracteres não deve ser menor que 5!");
            RuleFor(m => m.email)
                .NotEmpty()
                    .WithMessage("O email não deve ser nulo!")
                .EmailAddress()
                    .WithMessage("Digite um endereço de email válido!")
                .MaximumLength(100)
                    .WithMessage("O número de caracteres não deve ser maior que 100!")
                .MinimumLength(5)
                    .WithMessage("O número de caracteres não deve ser menor que 5!");
            RuleFor(m => m.password)
                .NotEmpty()
                    .WithMessage("A senha não deve ser nulo!")
                .MaximumLength(45)
                    .WithMessage("O número de caracteres não deve ser maior que 45!")
                .MinimumLength(6)
                    .WithMessage("O número de caracteres não deve ser menor que 6!");
        }
    }
}
