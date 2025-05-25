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

        public async Task<User> AddNewUserAsync(string email, string password, string name, string surname,
            DateOnly birthday, string phoneNumber, string gender, string address, string flatNumber, string zipCode,
            string place,
            CancellationToken cancellationToken = default)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var person = new Person()
                {
                    Name = name,
                    Surname = surname,
                    Birthday = birthday,
                    PhoneNumber = phoneNumber,
                    Gender = gender.Equals("M", StringComparison.OrdinalIgnoreCase) ? "Mezczyzna" : "Kobieta",
                    Weight = 0,
                    Height = 0,
                    Address = address,
                    FlatNumber = flatNumber,
                    ZipCode = zipCode,
                    Place = place
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
            return await _context.Users
                .Where(u => u.Email == email)
                .Select(u => u.IdUserNavigation)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(e => e.Email == email, cancellationToken);
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

        public async Task<string?> GetHashedPasswordAsync(string email, CancellationToken cancellationToken = default)
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

        public async Task<User?> GetUser(string email, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(e => e.Email == email)
                .FirstAsync();
            return user;
        }

        public async Task<bool> AddNewBoughtGymPass(int idPass, string email,
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
                    DateStart = DateOnly.FromDateTime(DateTime.Now),
                    DateEnd = DateOnly.FromDateTime(DateTime.Now).AddDays(gymPassDuration),
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

        public async Task<UserPass?> GetUserPass(string email, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await GetUser(email, cancellationToken);
                if (user == null)
                {
                    return null;
                }

                return await _context.UserPasses
                    .AsNoTracking()
                    .Where(e => e.IdUser == user.IdUser)
                    .Include(p => p.IdTypePassNavigation)
                    .Include(p => p.IdStatusNavigation)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user pass {Email}", email);
                return null;
            }
        }

        public async Task<List<GroupClass>> GetGroupClasses()
        {
            return await _context.GroupClasses
                .AsNoTracking()
                .Select(e => new GroupClass()
                {
                    IdGroup = e.IdGroup,
                    ClassType = e.ClassType,
                    StartDate = e.StartDate,
                    GroupSize = e.GroupSize,
                    IdCoach = e.IdCoach,
                    IdCoachNavigation = e.IdCoachNavigation,
                }).ToListAsync();
        }

        public async Task<bool> UserIsAlreadyInGroup(int groupId, string email,
            CancellationToken cancellationToken = default)
        {
            var user = await GetUser(email, cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var userIsSignedUp = await _context.ParticipationInClasses
                .AsNoTracking()
                .Where(e => e.IdUser == user.IdUser)
                .AnyAsync(e => e.IdGroup == groupId, cancellationToken);

            return userIsSignedUp;
        }

        public async Task<ParticipationInClass> SignUpForGroup(int groupId, string email,
            CancellationToken cancellationToken = default)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var user = await GetUser(email, cancellationToken);
                var getGroupName = await _context.GroupClasses
                    .AsNoTracking()
                    .Where(e => e.IdGroup == groupId)
                    .Select(e => e.ClassType)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var signUpForGroup = new ParticipationInClass()
                {
                    IdGroup = groupId,
                    IdUser = user.IdUser,
                };

                await _context.ParticipationInClasses.AddAsync(signUpForGroup, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                _logger.LogInformation("Successfully sign in to the group {Group}", getGroupName);
                return signUpForGroup;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Error in saving user to the group {Group}", groupId);
                throw;
            }
        }

        public async Task<List<ParticipationInClass>> GetUserParticipationInClass(string email,
            CancellationToken cancellationToken = default)
        {
            var user = await GetUser(email, cancellationToken);
            if (user == null)
            {
                throw new Exception("User is not found");
            }

            var useGroupActivities = await _context.ParticipationInClasses
                .AsNoTracking()
                .Where(e => e.IdUser == user.IdUser)
                .Include(p => p.IdGroupNavigation)
                .ToListAsync();
            if (useGroupActivities == null)
            {
                throw new Exception("User is not registered for any activities");
            }

            return useGroupActivities;
        }

        public async Task<bool> ChangePassword(string newPassword, string email,
            CancellationToken cancellationToken = default)
        {
            var user = await _context.Users
                .Where(e => e.Email == email)
                .FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                return false;
            }

            user.Password = newPassword;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> PassCancellation(int idUser, CancellationToken cancellationToken = default)
        {
            var userPass = _context.UserPasses.FirstOrDefault(e => e.IdUser == idUser);
            if (userPass == null)
            {
                return false;
            }

            _context.Remove(userPass);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<PurchasedCourse> AddCourse(int courseId, int userId,
            CancellationToken cancellationToken = default)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            

            var addNewCourseForUser = new PurchasedCourse()
            {
                IdCourse = courseId,
                IdUser = userId
            };
            await _context.PurchasedCourses.AddAsync(addNewCourseForUser, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return addNewCourseForUser;
        }

        public async Task<bool> CheckUserCourseExists(int courseId, int userId,
            CancellationToken cancellationToken = default)
        {
            return await _context.PurchasedCourses
                .AnyAsync(e => e.IdCourse == courseId && e.IdUser == userId, cancellationToken);
        }

        public async Task<List<PurchasedCourse>> GetPurchasedCourses(int userId,
            CancellationToken cancellationToken = default)
        {
            return await _context.PurchasedCourses
                .AsNoTracking()
                .Where(e => e.IdUser == userId)
                .Include(p => p.IdCourseNavigation)
                .ToListAsync();
        }
    }
}