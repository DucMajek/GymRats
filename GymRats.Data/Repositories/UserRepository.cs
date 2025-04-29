using GymRats.Data;
using GymRats.Data.Interfaces;
using GymRats.Data.Entities;
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

        public async Task<User> AddNewUserAsync(string email, string password, string name, string surname,
            CancellationToken cancellationToken = default)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var person = new Person()
                {
                    Name = name,
                    Surname = surname,
                    Birthday = null,
                    Address = string.Empty,
                    PhoneNumber = string.Empty,
                    Gender = string.Empty,
                    Weight = 0,
                    Height = 0,
                };

                await _context.People.AddAsync(person, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var user = new User()
                {
                    Email = email,
                    Password = password,
                    IdUser = person.IdPerson,
                    IdRole = 1
                };

                await _context.Users.AddAsync(user, cancellationToken);
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

        public async Task<Person?> GetUserPersonalDataAsync(string email,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.Users
                    .Where(u => u.Email == email)
                    .Select(u => u.IdUserNavigation)
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
                return await _context.Users
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
                var user = await _context.Users
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
                var userPassword = await _context.Users
                    .AsNoTracking()
                    .Where(e => e.Email == email)
                    .Select(e => e.Password)
                    .FirstOrDefaultAsync();

                return userPassword;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying password with email {Email}", email);
                throw;
            }
        }

        public async Task<User> GetUser(string email, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(e => e.Email == email)
                .FirstOrDefaultAsync();
            ;

            return user;
        }

        public async Task<bool> AddNewBoughtGymPass(int idPass, string email, DateOnly startDate, 
            CancellationToken cancellationToken = default)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var gymPassDuration = await _context.TypePasses
                    .AsNoTracking()
                    .Where(e => e.IdTypePass == idPass)
                    .Select(e => e.DurationPass)
                    .FirstOrDefaultAsync();
                
                var userId = await _context.Users
                    .AsNoTracking()
                    .Where(e => e.Email == email)
                    .Select(e => e.IdUser)
                    .FirstOrDefaultAsync();
                
                var newGymPass = new UserPass()
                {
                    DateStart = startDate,
                    DateEnd = startDate.AddDays(gymPassDuration),
                    IdTypePass = idPass,
                    IdUser = userId,
                    IdStatus = 1,
                };

                await _context.UserPasses.AddAsync(newGymPass, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Pass has been added to {Email}", idPass);
                await transaction.CommitAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Error buying pass {Email}", idPass);
                return false;
            }
        }
    }
}