using GymRats.Business.Interfaces;
using GymRats.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using GymRats.Presentation.DTOs;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        [AllowAnonymous]
        [HttpPost("/login")]
        public async Task<ActionResult<LoginResponse>> UserLogin(
            
            [FromBody]LoginRequest login,
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
                return Ok(new LoginResponse { Success = true, Token = token, Message = "Login successful" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for {Email}", login.Email);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during login");
            }
        }

        [HttpPost("/register")]
        public async Task<ActionResult<RegisterUserResponse>> Register(
            RegisterUserRequest newUser,
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

        [HttpGet("user/personal-data/{email}")]
        public async Task<ActionResult<Person>> GetUserPersonalData(
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

        [HttpPost("buyGymPass/{gymPassId}/{email}")]
        public async Task<ActionResult<UserPass>> BuyGymPass(int gymPassId, string email,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return BadRequest("Email is required");
                }

                var addGymPass = await _userService.BuyGymPass(gymPassId, email, cancellationToken);

                if (!addGymPass)
                {
                    return NotFound($"Cannot add gym pass for: {email}");
                }

                return Ok(addGymPass);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding data for email: {Email}", email);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while processing your request");
            }
        }

        [HttpGet("user/membership/{email}")]
        public async Task<ActionResult<UserPass>> GetUserPass(string email,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var gymPass = await _userService.UserPassData(email, cancellationToken);

                if (gymPass is null)
                {
                    return NotFound($"Cannot find gym pass for: {email}");
                }

                return Ok(gymPass);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving pass data for email: {Email}", email);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while processing your request");
            }
        }

        [HttpGet("user/groupClasses")]
        public async Task<ActionResult<List<GroupClass>>> GetGroupClasses()
        {
            var groupClasses = await _userService.GetGroupClasses();

            if (!groupClasses.Any())
            {
                return NotFound("No group classes found");
            }

            return Ok(groupClasses);
        }

        [HttpPost("user/signInToGroup/{email}/{groupId}")]
        public async Task<ActionResult<ParticipationInClass>> SignInToGroup(string email, int groupId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var signInToGroup = await _userService.SignUpForGroup(groupId, email, cancellationToken);
                if (signInToGroup == null)
                {
                    return NotFound($"User is already sign up for group {groupId}");
                }

                return Ok(signInToGroup);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error user is already sign up to the group {Group}", groupId);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while processing your request");
            }
        }
        
        [HttpGet("user/participationInClass/{email}")]
        public async Task<ActionResult<List<ParticipationInClass>>> GetParticipationInClasses(string email,
            CancellationToken cancellationToken = default)
        {
            var participationInClasses = await _userService.GetParticipationInClasses(email, cancellationToken);
            if (!participationInClasses.Any())
            {
                return NotFound("No participation in classes found");
            }
            return Ok(participationInClasses);
        }
    }
}