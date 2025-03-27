using GymRats.Data.Entities;
namespace Business.Interfaces;

public interface IUserServices
{
    Task<(bool success, string token)> LoginAsync(string email, string userPassowrd, CancellationToken cancellationToken = default);

    Task<bool> RegisterAsync(Uzytkownik newUser, Osoba newPerson,
        CancellationToken cancellationToken = default);

    Task<Osoba?> UserPersonData(Uzytkownik user, CancellationToken cancellationToken = default);
}