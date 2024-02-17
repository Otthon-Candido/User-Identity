using FluentValidation;
using FluentValidation.Results;
using User.Domain.Enum;
using User.Domain.Interfaces;
using User.Infra.Validator;
namespace User.Infra.Services
{

    public class ValidateFields : IValidadeFields
    {
        public string Validate(dynamic vo, ValidatorType validatorType)
        {
            ValidationResult validationResult = new ValidationResult();

            IValidator validator = CreateValidator(validatorType);

            if (validator != null)
            {
                validationResult = validator.Validate(vo);

                if (!validationResult.IsValid)
                {
                    // Concatenate all error messages
                    return string.Join("; ", validationResult.Errors.Select(error => error.ErrorMessage));
                }
            }
            else
            {
                return "Validator not found for the specified type.";
            }

            return string.Empty;
        }


        private static IValidator CreateValidator(ValidatorType validatorType)
        {
            switch (validatorType)
            {
                case ValidatorType.User:
                    return new UserValidator();
                case ValidatorType.Login:
                    return new LoginValidator();
                default:
                    return null;
            }
        }
    }
}