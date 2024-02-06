using System.ComponentModel.DataAnnotations;


namespace User.Domain.Models
{
    public class UserCreateModel

    {
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }

    }

}