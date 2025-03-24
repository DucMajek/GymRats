using gymrats.server.Models;
using Microsoft.AspNetCore.Mvc;
using gymrats.server.Models.DTOs;
using gymrats.server.Services;
using Microsoft.Extensions.Logging;

namespace gymrats.server.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IUserServices userService,
            ILogger<UserController> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("/login")]
        public async Task<ActionResult<LoginResponseDto>> SignIn(
            [FromBody] LoginRequestDto login,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid login request model state");
                    return BadRequest(ModelState);
                }

                var result = await _userService.LoginAsync(login, cancellationToken);

                if (result == null || !result.Success)
                {
                    _logger.LogWarning("Failed login attempt for {Email}", login.Email);
                    return Unauthorized(new { Message = "Invalid credentials" });
                }

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(1)
                };

                Response.Cookies.Append("jwt", result.Token, cookieOptions);

                _logger.LogInformation("Successful login for {Email}", login.Email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for {Email}", login.Email);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during login");
            }
        }

        [HttpPost("/register")]
        public async Task<ActionResult<RegisterUserResponseDto>> Register(
            [FromBody] RegisterUserRequestDto newUser,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid registration request model state");
                    return BadRequest(ModelState);
                }

                var result = await _userService.RegisterAsync(newUser, cancellationToken);

                if (!result.Success)
                {
                    _logger.LogWarning("Registration failed for {Email}: {Message}",
                        newUser.Email, result.Message);
                    return BadRequest(result);
                }

                _logger.LogInformation("New user registered: {Email}", newUser.Email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration for {Email}", newUser.Email);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during registration");
            }
        }

        [HttpGet("personal-data/{email}")]
        public async Task<ActionResult<Osoba>> GetUserPersonalData(
            [FromRoute] string email,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return BadRequest("Email is required");
                }

                var personalData = await _userService.UserPersonData(email, cancellationToken);

                if (personalData == null)
                {
                    return NotFound($"Personal data not found for email: {email}");
                }

                return Ok(personalData);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "User not found for email: {Email}", email);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation for email: {Email}", email);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving personal data for email: {Email}", email);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while processing your request");
            }
        }
    }
}