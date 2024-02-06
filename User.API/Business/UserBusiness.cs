using FluentValidation;
using FluentValidation.Results;
using Microsoft.IdentityModel.Tokens;
using User.Domain.Models;
using User.Domain.Validator;
using User.Infra.Exceptions;
using User.Infra.Repository;

namespace User.API.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;

        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void ActiveUser(ActiveUser request)
        {
            _userRepository.ActiveUser(request);
        }

        public void AddUser(UserCreateModel createUser)
        {
            string resultFromValidate = ValidateFields(createUser, new UserValidator());
            if (resultFromValidate.IsNullOrEmpty())
            {
                 _userRepository.AddUserAsync(createUser);
            }
            else
            {
                //Caso seja retornado algum erro na validação, retorne o erro
                throw new BadRequestException(resultFromValidate);
            }

        }

        private static string ValidateFields<T>(dynamic vo, T validator) where T : IValidator
        {
            ValidationResult validationResult = validator.Validate(vo);

            if (!validationResult.IsValid && validationResult.Errors.Any())
            {
                // Return the first error message if not valid
                return validationResult.Errors[0].ErrorMessage;
            }

            return string.Empty;
        }

        public string LoginAsync(LoginModel loginModel)
        {

            var resultFromValidate = ValidateFields(loginModel, new LoginValidator());
            if (resultFromValidate.IsNullOrEmpty())
            {
                return _userRepository.LoginAsync(loginModel);
            }
            else
            {
                //Caso seja retornado algum erro na validação, retorne o erro
                throw new BadRequestException(resultFromValidate);
            }
        }

        public void PasswordReset(PasswordReset request)
        {
            _userRepository.PasswordReset(request);
        }

        public void RequestPasswordReset(RequestPasswordReset request)
        {
             _userRepository.RequestPasswordReset(request);
        }
    }


}