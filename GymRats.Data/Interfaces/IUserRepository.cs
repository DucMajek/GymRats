using GymRats.Data.Entities;

namespace GymRats.Data.Interfaces;

public interface IUserRepository
{
    Task<bool> UserExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<User> AddNewUserAsync(string email, string password, string name, string surname,
        CancellationToken cancellationToken = default);

    Task<Person?> GetUserPersonalDataAsync(string email, 
        CancellationToken cancellationToken = default);
    Task<string?> GetHashedPasswordAsync(string email, CancellationToken cancellationToken = default);
    
    Task<User?> GetUser(string email, CancellationToken cancellationToken = default);

    Task<bool> AddNewBoughtGymPass(int gymPassId, string email, 
        CancellationToken cancellationToken = default);
    Task<UserPass?> GetUserPass(string email, CancellationToken cancellationToken = default);
    
    Task<List<GroupClass>> GetGroupClasses();
    
    Task<ParticipationInClass> SignUpForGroup(int groupId, string email, CancellationToken cancellationToken = default);
    Task<bool> UserIsAlreadyInGroup(int groupId, string email, CancellationToken cancellationToken = default);
    Task<List<ParticipationInClass>> GetUserParticipationInClass(string email, CancellationToken cancellationToken = default);
}