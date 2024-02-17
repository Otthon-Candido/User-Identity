using User.Domain.Models;

namespace User.Domain.Interfaces
{

    public interface IUserBusiness
    {

        public string AddUser(UserCreateModel createUser);

        public string LoginAsync(LoginModel loginModel);

        public void ActiveUser(ActiveUser request);

        public void RequestPasswordReset(RequestPasswordReset request);

        public void PasswordReset(PasswordReset request);

        

    }


}