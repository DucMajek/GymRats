using Business.Interfaces;
using GymRats.Data.Entities;
using GymRats.Data.Interfaces;
using GymRats.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace GymRats.Business.Services;

public class UserServices : IUserServices
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<UserServices> _logger;

    public UserServices(
        IUserRepository userRepository,
        ITokenGenerator tokenGenerator,
        IPasswordHasher passwordHasher,
        ILogger<UserServices> logger)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<(bool success, string token)> LoginAsync(string email, string userPassword, CancellationToken cancellationToken = default)
    {
        try
        {
            var userExists = await _userRepository.UserExistsAsync(email, cancellationToken);
            var userHashedPassword = await _userRepository.GetHashedPasswordAsync(email);
            var isValidPassword = _passwordHasher.VerifyPassword(userPassword, userHashedPassword);
            if (!userExists || !isValidPassword)
            {
                _logger.LogWarning("Failed login attempt for email: {Email}", email);
                return (false, null);
            }

            var token = _tokenGenerator.GenerateToken(email);

            _logger.LogInformation("Successful login for email: {Email}", email);
            return (true, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for email: {Email}", email);
            return (false, null);
        }
    }

    public async Task<bool> RegisterAsync(Uzytkownik newUser, Osoba newPerson,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var emailExists = await _userRepository.EmailExistsAsync(newUser.Email, cancellationToken);
            if (emailExists)
            {
                _logger.LogWarning("Registration attempt with existing email: {Email}", newUser.Email);
                return false;
            }

            newUser.Haslo = _passwordHasher.HashPassword(newUser.Haslo);
            var createdUser = await _userRepository.AddNewUserAsync(newUser, newPerson, cancellationToken);

            _logger.LogInformation("New user registered with email: {Email}", newUser.Email);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration for email: {Email}", newUser.Email);
            return false;
        }
    }

    public async Task<Osoba?> UserPersonData(Uzytkownik user, CancellationToken cancellationToken = default)
    {
        try
        {
            bool isValidPassword = _passwordHasher.VerifyPassword(user.Haslo, user.Haslo);
            return await _userRepository.GetUserPersonalDataAsync(user.Email, isValidPassword, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving gym pass for user {UserId}", user.Email);
            return null;
        }
    }
}