using User.Domain.Models;

namespace User.API.Business
{

    public interface IUserBusiness
    {

        public void AddUser(UserCreateModel createUser);

        public string LoginAsync(LoginModel loginModel);

        public void ActiveUser(ActiveUser request);

        public void RequestPasswordReset(RequestPasswordReset request);

        public void PasswordReset(PasswordReset request);

        

    }


}