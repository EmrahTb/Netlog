using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/apiUsers")]
    public class ApiUsersController : ControllerBase
    {
        private IApiUserService _ApiUserservice;
        public ApiUsersController(IApiUserService ApiUserservice)
        {
            _ApiUserservice = ApiUserservice;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _ApiUserservice.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}
