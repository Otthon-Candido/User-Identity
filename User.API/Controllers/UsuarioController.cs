using Microsoft.AspNetCore.Mvc;
using User.Domain.Models;
using User.API.Business;
using System.Text.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace User.API.Controllers
{

    [ApiController]
    [Route("Controller")]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }
        [HttpPost("/register")]
        public IActionResult AddUser(UserCreateModel createUser)
        {

            _userBusiness.AddUser(createUser);
            return Ok();

        }
        [HttpPost("/login")]
        public IActionResult LoginAsync(LoginModel loginModel)
        {

            var result = _userBusiness.LoginAsync(loginModel);
            string resultJson = JsonSerializer.Serialize(result);
            return Ok(resultJson);

        }

        [HttpPost("/active")]
        public IActionResult ActiveUser(ActiveUser request)
        {

            _userBusiness.ActiveUser(request);
            return Ok();
        }

        [HttpPost("/requestPasswordReset")]
        public IActionResult RequestPasswordReset(RequestPasswordReset request)
        {

            _userBusiness.RequestPasswordReset(request);
            return Ok();
        }


        [HttpPost("/passwordReset")]
        public IActionResult PasswordReset(PasswordReset request)
        {

            _userBusiness.PasswordReset(request);
            return Ok();
        }
    }

}