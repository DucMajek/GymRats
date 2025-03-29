using Business.Interfaces;
using GymRats.Business.Interfaces;
using GymRats.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using GymRats.Data.Entities;
using GymRats.Presentation.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace GymRats.Presentation.Controllers
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

        [AllowAnonymous]
        [HttpPost("/login")]
        public async Task<ActionResult<LoginResponse>> UserLogin(
            [FromBody] LoginRequest login,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid login request model state");
                    return BadRequest(ModelState);
                }

                var (success, token, user) = await _userService.LoginAsync(login.Email, 
                    login.Password, cancellationToken);

                if (!success)
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

                Response.Cookies.Append("jwt", token, cookieOptions);

                _logger.LogInformation("Successful login for {Email}", login.Email);
                return Ok(new LoginResponse {Success = true, Token = token, Message = "Login successful"});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for {Email}", login.Email);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during login");
            }
        }

        [HttpPost("/register")]
        public async Task<ActionResult<RegisterUserResponse>> Register(
            [FromBody] RegisterUserRequest newUser,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid registration request model state");
                    return BadRequest(ModelState);
                }

                var result = await _userService.RegisterAsync(newUser.Email, newUser.Password,
                    newUser.Name, newUser.Surname,
                    cancellationToken);

                if (!result)
                {
                    _logger.LogWarning("Registration failed for {Email}",
                        newUser.Email);
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

        /*[HttpGet("personal-data/{email}")]
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
        }*/
    }
}