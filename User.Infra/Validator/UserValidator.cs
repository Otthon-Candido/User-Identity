using FluentValidation;
using User.Domain.Models;

namespace User.Infra.Validator
{
    public class UserValidator : AbstractValidator<UserCreateModel>
    {
        public UserValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório")
                .EmailAddress().WithMessage("E-mail não é valido")
                .Matches("^[a-zA-Z0-9@._]*$").WithMessage("Caracteres inválidos no e-mail")
                .MaximumLength(40).WithMessage("E-mail deve conter no máximo 40 caracteres");

            RuleFor(user => user.NickName)
                .NotEmpty().WithMessage("Nome de usuário é obrigatório")
                .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Caracteres inválidos no nome do usuário")
                .MaximumLength(20).WithMessage("Nome de usuário deve conter no máximo 20 caracteres");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Senha é obrigatório")
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$&*~]).{8,}$")
                .WithMessage("Senha deve conter no mínimo 8 caracteres, letra maiúscula, numeros e caracteres especiais")
                .MaximumLength(60).WithMessage("Senha deve conter no máximo 60 caracteres");

            RuleFor(user => user.RePassword)
                .NotEmpty().WithMessage("Confirmar senha é obrigatório")
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$&*~]).{8,}$")
                .WithMessage("Senha deve conter no mínimo 8 caracteres, letra maiúscula, numeros e caracteres especiais")
                .MaximumLength(60).WithMessage("Senha deve conter no máximo 60 caracteres")
                .Equal(user => user.Password)
                .WithMessage("As senhas devem ser iguais");
        }
    }
}