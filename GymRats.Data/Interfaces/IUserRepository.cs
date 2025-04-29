using GymRats.Data;
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
    Task<string> GetHashedPasswordAsync(string email, CancellationToken cancellationToken = default);
    
    Task<User> GetUser(string email, CancellationToken cancellationToken = default);

    Task<bool> AddNewBoughtGymPass(int gymPassId, string email, DateOnly startDate, 
        CancellationToken cancellationToken = default);
}