using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace User.Infra.Entities
{


    public class UserEntity : IdentityUser

    {
        public string NickName { get; set; }
        public UserEntity() : base() { }

    }

}