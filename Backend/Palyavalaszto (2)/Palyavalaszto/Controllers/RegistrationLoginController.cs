using JWTAuth.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Palyavalaszto.Services;

namespace Palyavalaszto.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrationLoginController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserRegistrationLoginController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> UserRegistration(UserRegistrationDto userRegistration)
        {
            var result = await _userService.RegisterNewUserAsync(userRegistration);
            if (result.isUserRegistered)
            {

                return Ok(result.Message);
            }
            ModelState.AddModelError("Email", result.Message);
            return BadRequest(ModelState);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            var result = await _userService.LoginUserAsync(userLogin);
            if (result.isUserLoggedIn)
            {
               
                return Ok(new { Token = result.jwtToken });
            }
            else
            {
                Console.WriteLine("Sikertelen bejelentkezés a következő felhasználónévvel: " + userLogin.Email);
                ModelState.AddModelError("Email", result.jwtToken);
                return BadRequest(ModelState);
            }
        }

    }
}