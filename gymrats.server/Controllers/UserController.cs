using Microsoft.AspNetCore.Mvc;
using gymrats.server.Models.DTOs;
using gymrats.server.Services;

namespace gymrats.server.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserServices _userService;

        public UserController(IConfiguration configuration, IUserServices userService)
        {
            _configuration = configuration;
            _userService = userService;
        }


        [HttpPost("/login")]
        public async Task<IActionResult> SignIn(LoginRequestDto login)
        {
            var result = await _userService.GetUser(login);
            if(result.Token != null)
                return Ok(new {result.Token});

            return Unauthorized(result.Message);
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterUserRequestDto newUser)
        {
            var result = await _userService.RegisterUser(newUser);
            if (result.Status == "Error")
            {
                return BadRequest(new { result.Message, result.Status });
            }

            return Ok(new { result.Message, result.Status });
        }



    }
}
