using GymRats.Data.Entities;

namespace GymRats.Data.Interfaces;

public interface IUserRepository
{
    Task<bool> UserExists(string email, CancellationToken cancellationToken = default);
    Task<bool> EmailExists(string email, CancellationToken cancellationToken = default);

    Task<User> AddNewUser(string email, string password, string name, string surname,
        string birthday, string phoneNumber, string gender, string address, string flatNumber, string zipCode,
        string place,
        CancellationToken cancellationToken = default);

    Task<Person?> GetUserPersonalData(string email,
        CancellationToken cancellationToken = default);

    Task<string?> GetHashedPassword(string email, CancellationToken cancellationToken = default);

    Task<User?> GetUser(string email, CancellationToken cancellationToken = default);

    Task<bool> AddNewBoughtGymPass(int gymPassId, string email,
        CancellationToken cancellationToken = default);

    Task<UserPass?> GetUserPass(string email, CancellationToken cancellationToken = default);

    Task<List<GroupClass>> GetGroupClasses();

    Task<ParticipationInClass> SignUpForGroup(int groupId, string email, CancellationToken cancellationToken = default);
    Task<bool> UserIsAlreadyInGroup(int groupId, string email, CancellationToken cancellationToken = default);

    Task<List<ParticipationInClass>> GetUserParticipationInClass(string email,
        CancellationToken cancellationToken = default);

    Task<bool> UpdatePassword(string newPassword, string email, CancellationToken cancellationToken = default);
    Task<bool> DeleteGymPass(int idUser, CancellationToken cancellationToken = default);
    Task<PurchasedCourse> AddCourse(int courseId, int userId, CancellationToken cancellationToken = default);
    Task<bool> IsCoursePurchasedByUser(int courseId, int userId, CancellationToken cancellationToken = default);
    Task<List<PurchasedCourse>> GetUserPurchasedCourses(int userId, CancellationToken cancellationToken = default);
    Task<bool> IsTrainer(int userId, CancellationToken cancellationToken = default);

    Task<GroupClass> AddNewGroupClass(int coachId, string classType, DateTime start, int duration, int groupSize,
        CancellationToken cancellationToken = default);

    Task<List<PersonalTraining>> GetPersonalTraining(int userId, CancellationToken cancellationToken = default);
    Task<List<Coach>> GetCoaches();
    Task<bool> DeleteParticipationInClass(int groupId, int userId, CancellationToken cancellationToken = default);
}