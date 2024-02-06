using System.ComponentModel.DataAnnotations;


namespace User.Domain.Models
{
    public class UserModel

    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        public string RePassword { get; set; }

    }

}