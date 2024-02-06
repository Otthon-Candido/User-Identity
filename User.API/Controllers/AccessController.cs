using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace User.API.Controllers
{

    [ApiController]
    [Route("Controller")]
    public class AccesController : ControllerBase
    {

        [HttpGet("/Access")]
        [Authorize (Roles = "admin")]
        public IActionResult Get()
        {
            return Ok();
        }

    }

}