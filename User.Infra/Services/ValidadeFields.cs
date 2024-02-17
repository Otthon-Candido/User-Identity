using FluentValidation;
using FluentValidation.Results;
using User.Domain.Enum;
using User.Domain.Interfaces;
using User.Domain.Models;
using User.Infra.Exceptions;
using User.Infra.Validator;
namespace User.Infra.Services
{

    public class ValidateFields : IValidadeFields
    {

        public string Validate(LoginModel vo, ValidatorType validatorType)
        {
            return ValidateInternal(vo, validatorType);
        }

        public string Validate(UserCreateModel vo, ValidatorType validatorType)
        {
            return ValidateInternal(vo, validatorType);
        }

        private static string ValidateInternal<T>(T vo, ValidatorType validatorType)
        {
            ValidationResult validationResult = new();

            var validator = CreateValidator<T>(validatorType);

            if (validator != null)
            {
                validationResult = validator.Validate(vo);

                if (!validationResult.IsValid)
                {
                    return string.Join("; ", validationResult.Errors.Select(error => error.ErrorMessage));
                }
            }
            else
            {
                throw new InternalException("Ocorreu um erro inesperado, tente novamente. Caso o problema persista, entre em contato com o suporte.");
            }

            return string.Empty;
        }


        private static AbstractValidator<T> CreateValidator<T>(ValidatorType validatorType)
        {
            switch (validatorType)
            {
                case ValidatorType.Login when typeof(T) == typeof(LoginModel):
                    return (AbstractValidator<T>)(object)new LoginValidator();
                case ValidatorType.User when typeof(T) == typeof(UserCreateModel):
                    return (AbstractValidator<T>)(object)new UserValidator();
                default:
                    return null;
            }
        }

    }
}