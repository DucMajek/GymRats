using GymRats.Data.Entities;
namespace GymRats.Business.Interfaces;

public interface IUserServices
{
    Task<(bool success, string token, Uzytkownik user)> LoginAsync(string email, string userPassword, CancellationToken cancellationToken = default);

    Task<bool> RegisterAsync(string email, string password, string name, string surname,
        CancellationToken cancellationToken = default);

    Task<Osoba?> UserPersonData(Uzytkownik user, CancellationToken cancellationToken = default);
}