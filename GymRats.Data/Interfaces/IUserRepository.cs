using GymRats.Data.Entities;

namespace GymRats.Data.Interfaces;

public interface IUserRepository
{
    Task<bool> UserExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<Uzytkownik> AddNewUserAsync(string email, string password, string name, string surname,
        CancellationToken cancellationToken = default);

    Task<Osoba?> GetUserPersonalDataAsync(string email, bool isValidPassword, 
        CancellationToken cancellationToken = default);
    Task<string> GetHashedPasswordAsync(string email, CancellationToken cancellationToken = default);
}