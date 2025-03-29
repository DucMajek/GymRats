using GymRats.Data.Entities;
using GymRats.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymRats.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GymRatsContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(
            GymRatsContext context,
            ILogger<UserRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Uzytkownik> AddNewUserAsync(string email, string password, string name, string surname,
            CancellationToken cancellationToken = default)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var person = new Osoba
                {
                    Imie = name,
                    Nazwisko = surname,
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
                    Email = email,
                    Haslo = password,
                    OsobaIdOsoba = person.IdOsoba
                };

                await _context.Uzytkowniks.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                _logger.LogInformation("Successfully created new user with email {Email}", email);
                return user;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Error creating new user with email {Email}", email);
                throw;
            }
        }

        public async Task<Osoba?> GetUserPersonalDataAsync(string email, bool isValidPassword,
            CancellationToken cancellationToken = default)
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

        public async Task<bool> UserExistsAsync(string email,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _context.Uzytkowniks
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Email == email, cancellationToken);

                if (user == null)
                {
                    _logger.LogWarning("Login attempt for non-existent email {Email}", email);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying user with email {Email}", email);
                throw;
            }
        }

        public async Task<string> GetHashedPasswordAsync(string email, CancellationToken cancellationToken = default)
        {
            try
            {
                var userPassword = await _context.Uzytkowniks
                    .AsNoTracking()
                    .Where(e => e.Email == email)
                    .Select(e => e.Haslo)
                    .FirstOrDefaultAsync();

                return userPassword;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying password with email {Email}", email);
                throw;
            }
        }

        public async Task<Uzytkownik> GetUser(string email, CancellationToken cancellationToken = default)
        {
            var user = await _context.Uzytkowniks
                .AsNoTracking()
                .Where(e => e.Email == email)
                .FirstOrDefaultAsync();;
            
            return user;
        }
    }
}