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
        public async Task<IActionResult> SignIn([FromBody] LoginRequestDto login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Login(login);

            if (result == null)
                return Unauthorized("Wrong username or password");

            // Ustawienie tokena jako ciasteczka
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            };

            Response.Cookies.Append("jwt", result.Token, cookieOptions);

            return Ok(new { result.Token });
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterUserRequestDto newUser)
        {
            var result = await _userService.Register(newUser);
            if (result.Status == "Error")
            {
                return BadRequest(new { result.Message, result.Status });
            }

            return Ok(new { result.Message, result.Status });
        }



    }
}
