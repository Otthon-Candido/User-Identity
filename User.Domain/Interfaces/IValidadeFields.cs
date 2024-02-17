using User.Domain.Enum;
using User.Domain.Models;

namespace User.Domain.Interfaces
{
    public interface IValidadeFields
    {
        string Validate(LoginModel vo, ValidatorType validatorType);
        string Validate(UserCreateModel vo, ValidatorType validatorType);
    }

}