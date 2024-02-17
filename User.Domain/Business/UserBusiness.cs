using User.Domain.Enum;
using User.Domain.Interfaces;
using User.Domain.Models;


namespace User.Domain.Business
{
 
    public class UserBusiness : IUserBusiness
    {
        private readonly IValidadeFields _validadeFields;
        private readonly IUserRepository _userRepository;

        public UserBusiness(IUserRepository userRepository, IValidadeFields validadeFields)
        {
            _validadeFields = validadeFields;
            _userRepository = userRepository;
        }

        public void ActiveUser(ActiveUser request)
        {
            _userRepository.ActiveUser(request);
        }

        public string? AddUser(UserCreateModel createUser)
        {
            string resultFromValidate =  _validadeFields.Validate(createUser, ValidatorType.User); 
            if (string.IsNullOrEmpty(resultFromValidate))
            {
                _userRepository.AddUserAsync(createUser);
                return null;
            }
            else
            {
                return $"Erro: {resultFromValidate}";

            }
        }

      

        public string LoginAsync(LoginModel loginModel)
        {

                  string resultFromValidate =  _validadeFields.Validate(loginModel, ValidatorType.Login); 
            if (string.IsNullOrEmpty(resultFromValidate))
            {
                return _userRepository.LoginAsync(loginModel);
            }
            else
            {
                return $"Erro: {resultFromValidate}";
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