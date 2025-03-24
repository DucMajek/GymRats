using gymrats.server.Models;
using gymrats.server.Models.DTOs;
using gymrats.server.Repositories;
using Microsoft.Extensions.Logging;

namespace gymrats.server.Services;

public interface IUserServices
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto login, CancellationToken cancellationToken = default);

    Task<RegisterUserResponseDto> RegisterAsync(RegisterUserRequestDto newUser,
        CancellationToken cancellationToken = default);
    
    Task<Osoba?> UserPersonData(string email, CancellationToken cancellationToken = default);
}

public class UserServices : IUserServices
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly ILogger<UserServices> _logger;

    public UserServices(
        IUserRepository userRepository,
        ITokenGenerator tokenGenerator,
        ILogger<UserServices> logger)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto login, CancellationToken cancellationToken = default)
    {
        try
        {
            var userExists = await _userRepository.UserExistsAsync(login, cancellationToken);

            if (!userExists)
            {
                _logger.LogWarning("Failed login attempt for email: {Email}", login.Email);
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Invalid email or password"
                };
            }

            var token = _tokenGenerator.GenerateToken(login.Email);

            _logger.LogInformation("Successful login for email: {Email}", login.Email);
            return new LoginResponseDto
            {
                Success = true,
                Token = token,
                Message = "Login successful"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for email: {Email}", login.Email);
            return new LoginResponseDto
            {
                Success = false,
                Message = "An error occurred during login"
            };
        }
    }

    public async Task<RegisterUserResponseDto> RegisterAsync(RegisterUserRequestDto newUser,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var emailExists = await _userRepository.EmailExistsAsync(newUser.Email, cancellationToken);
            if (emailExists)
            {
                _logger.LogWarning("Registration attempt with existing email: {Email}", newUser.Email);
                return new RegisterUserResponseDto
                {
                    Success = false,
                    Message = "Email already exists"
                };
            }

            var createdUser = await _userRepository.AddNewUserAsync(newUser, cancellationToken);

            _logger.LogInformation("New user registered with email: {Email}", newUser.Email);
            return new RegisterUserResponseDto
            {
                Success = true,
                Message = "Registration successful!",
                UserId = createdUser.IdUzytkownika // Assuming Uzytkownik has an Id property
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration for email: {Email}", newUser.Email);
            return new RegisterUserResponseDto
            {
                Success = false,
                Message = "An error occurred during registration"
            };
        }
    }

    public async Task<Osoba?> UserPersonData(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _userRepository.GetUserPersonalDataAsync(email, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving gym pass for user {UserId}", email);
            return null;
        }
    }
}