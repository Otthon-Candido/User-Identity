using Microsoft.AspNetCore.Mvc;
using User.Domain.Models;
using User.Domain.Interfaces;
using User.Infra.Exceptions;

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
            var result = _userBusiness.AddUser(createUser);
            if (result.StartsWith("Erro:"))
            {
                throw new BadRequestException(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost("/login")]
        public IActionResult LoginAsync(LoginModel loginModel)
        {
            var result = _userBusiness.LoginAsync(loginModel);

            if (result.StartsWith("Erro:"))
            {
                throw new BadRequestException(result);
            }
            else
            {
                return Ok(result);
            }
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