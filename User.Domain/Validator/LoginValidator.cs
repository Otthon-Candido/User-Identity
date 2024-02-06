using FluentValidation;
using User.Domain.Models;

namespace User.Domain.Validator
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("Login é obrigatório")
                .Matches("^[a-zA-Z0-9@._]*$").WithMessage("Caracteres inválidos no login")
                .MaximumLength(40).WithMessage("E-mail deve conter no máximo 40 caracteres");


            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Senha é obrigatório")
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$&*~]).{8,}$")
                .WithMessage("Senha deve conter no mínimo 8 caracteres, letra maiúscula, numeros e caracteres especiais")
                .MaximumLength(60).WithMessage("Senha deve conter no máximo 60 caracteres");

        }
    }
}