using Humanity.Application.Interfaces;
using Humanity.Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Humanity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController(IUserService identityService) : Controller
    {
        //private readonly IUserService _userService;

        //public UserController(IUserService userService)
        //{
        //    _userService = userService;
        //}

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await identityService.LoginAsync(request);
            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest();
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var response = await identityService.RegisterAsync(request);

            return StatusCode(response.Succeeded ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest,
                response);

            //if (response.Succeeded)
            //{
            //    return Ok(response);
            //}

            //return BadRequest(response);
        }
    }
}
