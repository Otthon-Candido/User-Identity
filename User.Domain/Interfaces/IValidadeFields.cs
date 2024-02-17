using User.Domain.Enum;

namespace User.Domain.Interfaces
{
public interface IValidadeFields
{
      string Validate(dynamic vo, ValidatorType validatorType);
}
}