using GymRats.Data.Entities;

namespace GymRats.Data.Interfaces;

public interface IUserRepository
{
    Task<bool> UserExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);

    Task<User> AddNewUserAsync(string email, string password, string name, string surname,
        DateOnly birthday, string phoneNumber, string gender, string address, string flatNumber, string zipCode,
        string place,
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
    Task<bool> ChangePassword(string newPassword, string email, CancellationToken cancellationToken = default);
    Task<bool> PassCancellation(int idUser, CancellationToken cancellationToken = default);
    Task<PurchasedCourse> AddCourse(int courseId, int userId, CancellationToken cancellationToken = default);
    Task<bool> CheckUserCourseExists(int courseId, int userId, CancellationToken cancellationToken = default);
    Task<List<PurchasedCourse>> GetPurchasedCourses(int userId, CancellationToken cancellationToken = default);
}