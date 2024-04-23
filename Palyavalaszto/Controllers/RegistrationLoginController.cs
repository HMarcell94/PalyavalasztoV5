using JWTAuth.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Palyavalaszto.Dto;
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
            APIResponse response = new APIResponse();
            var result = await _userService.RegisterNewUserAsync(userRegistration);
            if (result.isUserRegistered)
            {
                return Ok(result);
            }
            response.StatusCode = 1;
            response.Message = result.Message;
            ModelState.AddModelError("Email", result.Message);
            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            APIResponse<string> response = new APIResponse<string>();
            var result = await _userService.LoginUserAsync(userLogin);
            if (result.isUserLoggedIn)
            {
                response.Data = result.jwtToken;
                return Ok(response);
            }
            else
            {
                response.Message = "Sikertelen bejelentkezés a következő felhasználónévvel: " + userLogin.Email;
                response.StatusCode = 1;
                Console.WriteLine("Sikertelen bejelentkezés a következő felhasználónévvel: " + userLogin.Email);
                //ModelState.AddModelError("Email", result.jwtToken);
                return BadRequest(response);
            }
        }

    }
}