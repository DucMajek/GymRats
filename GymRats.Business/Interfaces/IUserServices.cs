using GymRats.Data.Entities;

namespace GymRats.Business.Interfaces;

public interface IUserServices
{
    Task<(bool success, string token, User user)> Login(string email, string userPassword,
        CancellationToken cancellationToken = default);

    Task<bool> Register(string email, string password, string name, string surname,
        DateOnly birthday, string phoneNumber, string gender, string address, string flatNumber, string zipCode,
        string place,
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

    Task<bool> ChangeUserPassword(string email, string oldPassword, string newPassword,
        CancellationToken cancellationToken = default);

    Task<bool> PassCancellation(string email,
        CancellationToken cancellationToken = default);

    Task<PurchasedCourse> AddCourse(int courseId, string email,
        CancellationToken cancellationToken = default);
    
    Task<List<PurchasedCourse>> GetCourses(string email, CancellationToken cancellationToken = default);
    
    Task<GroupClass> AddNewGroupClass(string email, string classType, DateTime start, int duration, int groupSize,
        CancellationToken cancellationToken = default);

    Task<List<PersonalTraining>> GetPersonalTrainings(string email, CancellationToken cancellationToken = default);
    Task<List<Coach>> GetCoachesList();
}