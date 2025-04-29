using GymRats.Data.Entities;
namespace GymRats.Business.Interfaces;

public interface IUserServices
{
    Task<(bool success, string token, User user)> LoginAsync(string email, string userPassword, 
        CancellationToken cancellationToken = default);

    Task<bool> RegisterAsync(string email, string password, string name, string surname,
        CancellationToken cancellationToken = default);

    Task<Person?> UserPersonData(string email, CancellationToken cancellationToken = default);

    Task<bool> BuyGymPass(int gymPassId, string email, DateOnly startDate, 
        CancellationToken cancellationToken = default);
}