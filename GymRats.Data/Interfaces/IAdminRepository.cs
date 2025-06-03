using GymRats.Data.Entities;

namespace GymRats.Data.Interfaces;

public interface IAdminRepository
{
    Task<bool> IsAdmin(int userId, CancellationToken cancellationToken = default);

    Task<FoodEbook> AddFoodEbook(string calories, string dietType, byte[] EbookFile,
        CancellationToken cancellationToken = default);

    Task<TrainingPlan> AddTrainingPlan(string trainingPlanName, byte[] TrainingPlanFile,
        CancellationToken cancellationToken = default);

    Task<TypePass> UpdateGymPassPrice(int gymPassId, int newPrice, CancellationToken cancellationToken = default);

    Task<TypePass> AddTypePass(string gymPassName, int price, int durationPass, string description,
        CancellationToken cancellationToken = default);

    Task<TrainerCourse> AddTrainerCourse(string courseName, string duration, string description, int coachId,
        CancellationToken cancellationToken = default);
}