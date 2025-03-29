using Business.Interfaces;
using GymRats.Business.Interfaces;
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

    public async Task<(bool success, string token, Uzytkownik user)> LoginAsync(
        string email, 
        string userPassword, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var userExists = await _userRepository.UserExistsAsync(email, cancellationToken);
            if (!userExists)
            {
                _logger.LogWarning("User does not exist: {Email}", email);
                return (false, null, null);
            }

            // 2. Pobierz hasło i zweryfikuj
            var userHashedPassword = await _userRepository.GetHashedPasswordAsync(email);
            if (string.IsNullOrEmpty(userHashedPassword))
            {
                _logger.LogWarning("No password found for user: {Email}", email);
                return (false, null, null);
            }

            var isValidPassword = _passwordHasher.VerifyPassword(userPassword, userHashedPassword);
            if (!isValidPassword)
            {
                _logger.LogWarning("Invalid password for user: {Email}", email);
                return (false, null, null);
            }
            
            var user = await _userRepository.GetUser(email);
            if (user == null)
            {
                _logger.LogError("User data not found despite successful auth: {Email}", email);
                return (false, null, null);
            }
            
            var token = _tokenGenerator.GenerateToken(email);

            _logger.LogInformation("Successful login: {Email}", email);
            return (true, token, user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login error for {Email}", email);
            return (false, null, null);
        }
    }

    public async Task<bool> RegisterAsync(string email, string password, string name, string surname,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var emailExists = await _userRepository.EmailExistsAsync(email, cancellationToken);
            if (emailExists)
            {
                _logger.LogWarning("Registration attempt with existing email: {Email}", email);
                return false;
            }

            password = _passwordHasher.HashPassword(password);
            await _userRepository.AddNewUserAsync(email, password, name, surname,
                cancellationToken);

            _logger.LogInformation("New user registered with email: {Email}", email);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration for email: {Email}", email);
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