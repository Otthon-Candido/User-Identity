using System.ComponentModel.DataAnnotations;

namespace User.Domain.Models
{


    public class PasswordReset
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string IdUser { get; set; }
        public string Token { get; set; }
    }


}