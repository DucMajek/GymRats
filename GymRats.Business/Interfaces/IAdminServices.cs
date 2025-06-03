using GymRats.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace GymRats.Business.Interfaces;

public interface IAdminServices
{

    Task<FoodEbook> AddNewFoodEbook(string calories, string dietType, IFormFile EbookFile,
        CancellationToken cancellationToken = default);

    Task<TrainingPlan> AddNewTrainingPlan(string trainingPlanName, IFormFile TrainingPlanFile,
        CancellationToken cancellationToken = default);

    Task<TypePass> ChangeGymPassPrice(int gymPassId, int newPrice, CancellationToken cancellationToken = default);

    Task<TypePass> AddNewTypePass(string gymPassName, int price, int durationPass, string description,
        CancellationToken cancellationToken = default);

    Task<TrainerCourse> AddNewTrainerCourse(string courseName, string duration, string description, int coachId,
        CancellationToken cancellationToken = default);
}