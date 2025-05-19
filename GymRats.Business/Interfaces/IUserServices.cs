using GymRats.Data.Entities;

namespace GymRats.Business.Interfaces;

public interface IUserServices
{
    Task<(bool success, string token, User user)> LoginAsync(string email, string userPassword,
        CancellationToken cancellationToken = default);

    Task<bool> RegisterAsync(string email, string password, string name, string surname,
        CancellationToken cancellationToken = default);

    Task<Person?> UserPersonData(string email, CancellationToken cancellationToken = default);

    Task<bool> BuyGymPass(int gymPassId, string email,
        CancellationToken cancellationToken = default);

    Task<UserPass?> UserPassData(string email, CancellationToken cancellationToken = default);
    Task<List<GroupClass>> GetGroupClasses();

    Task<ParticipationInClass?>
        SignUpForGroup(int groupId, string email, CancellationToken cancellationToken = default);

    Task<List<ParticipationInClass>> GetParticipationInClasses(string email,
        CancellationToken cancellationToken = default);
}