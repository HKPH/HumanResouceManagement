using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AccountDto request)
        {
            var response =_authService.Authenticate(request);
            if (response == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            return Ok(response);
        }
    }

}
