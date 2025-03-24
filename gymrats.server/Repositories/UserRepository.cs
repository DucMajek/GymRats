using gymrats.server.Models;
using gymrats.server.Models.DTOs;
using gymrats.server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace gymrats.server.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(LoginRequestDto login, CancellationToken cancellationToken = default);
        Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
        Task<Uzytkownik> AddNewUserAsync(RegisterUserRequestDto newUser, CancellationToken cancellationToken = default);

        Task<Osoba?> GetUserPersonalDataAsync(string email, CancellationToken cancellationToken = default);
    }

    public class UserRepository : IUserRepository
    {
        private readonly GymRatsContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(
            GymRatsContext context,
            IPasswordHasher passwordHasher,
            ILogger<UserRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Uzytkownik> AddNewUserAsync(RegisterUserRequestDto newUser,
            CancellationToken cancellationToken = default)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var person = new Osoba
                {
                    Imie = newUser.Imie,
                    Nazwisko = newUser.Nazwisko,
                    DataUrodzenia = null,
                    Adres = string.Empty,
                    NrTel = string.Empty,
                    Plec = string.Empty,
                    Waga = 0,
                    Wzrost = 0,
                };

                await _context.Osobas.AddAsync(person, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var user = new Uzytkownik
                {
                    Email = newUser.Email,
                    Haslo = _passwordHasher.HashPassword(newUser.Password),
                    OsobaIdOsoba = person.IdOsoba
                };

                await _context.Uzytkowniks.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                _logger.LogInformation("Successfully created new user with email {Email}", newUser.Email);
                return user;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Error creating new user with email {Email}", newUser.Email);
                throw;
            }
        }

        public async Task<Osoba?> GetUserPersonalDataAsync(string email, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.Uzytkowniks
                    .Where(u => u.Email == email)
                    .Select(u => u.OsobaIdOsobaNavigation)
                    .FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if email {Email} exists", email);
                throw;
            }
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.Uzytkowniks
                    .AsNoTracking()
                    .AnyAsync(e => e.Email == email, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if email {Email} exists", email);
                throw;
            }
        }

        public async Task<bool> UserExistsAsync(LoginRequestDto login, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _context.Uzytkowniks
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Email == login.Email, cancellationToken);

                if (user == null)
                {
                    _logger.LogWarning("Login attempt for non-existent email {Email}", login.Email);
                    return false;
                }

                bool isValidPassword = _passwordHasher.VerifyPassword(login.Password, user.Haslo);

                if (!isValidPassword)
                {
                    _logger.LogWarning("Invalid password attempt for email {Email}", login.Email);
                }

                return isValidPassword;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying user with email {Email}", login.Email);
                throw;
            }
        }
    }
}