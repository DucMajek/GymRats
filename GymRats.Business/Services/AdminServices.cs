using GymRats.Business.Exceptions;
using GymRats.Business.Interfaces;
using GymRats.Data.Entities;
using GymRats.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GymRats.Business.Services;

public class AdminServices : IAdminServices
{
    private readonly IAdminRepository _adminRepository;
    private readonly ILogger<AdminServices> _logger;

    public AdminServices(
        IAdminRepository adminRepository,
        ILogger<AdminServices> logger)
    {
        _adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<FoodEbook> AddNewFoodEbook(string calories, string dietType, IFormFile EbookFile,
        CancellationToken cancellationToken = default)
    {
        if (EbookFile == null || EbookFile.Length == 0 || !EbookFile.ContentType.Equals("application/pdf"))
        {
            throw new InvalidFileFormatException("Invalid PDF file.");
        }

        byte[] fileBytes;
        using (var memoryStream = new MemoryStream())
        {
            await EbookFile.CopyToAsync(memoryStream, cancellationToken);
            fileBytes = memoryStream.ToArray();
        }

        return await _adminRepository.AddFoodEbook(calories, dietType, fileBytes, cancellationToken);
    }

    public async Task<TrainingPlan> AddNewTrainingPlan(string trainingPlanName, IFormFile TrainingPlanFile,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(trainingPlanName))
            throw new ArgumentNullException("Training plan name cannot be null or empty.");
        if (TrainingPlanFile == null || TrainingPlanFile.Length == 0 ||
            !TrainingPlanFile.ContentType.Equals("application/pdf"))
        {
            throw new InvalidFileFormatException("Invalid PDF file.");
        }

        byte[] fileBytes;
        using (var memoryStream = new MemoryStream())
        {
            await TrainingPlanFile.CopyToAsync(memoryStream, cancellationToken);
            fileBytes = memoryStream.ToArray();
        }

        return await _adminRepository.AddTrainingPlan(trainingPlanName, fileBytes, cancellationToken);
    }

    public async Task<TypePass> ChangeGymPassPrice(int gymPassId, int newPrice,
        CancellationToken cancellationToken = default)
    {
        if (newPrice <= 0)
        {
            throw new ArgumentException("The price must be greater zero.");
        }

        return await _adminRepository.UpdateGymPassPrice(gymPassId, newPrice, cancellationToken);
    }

    public async Task<TypePass> AddNewTypePass(string gymPassName, int price, int durationPass, string description,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(gymPassName))
            throw new ArgumentNullException("Gym pass name cannot be null or empty.");

        if (price < 0)
            throw new ArgumentException("The price must be greater zero.");

        if (durationPass < 0)
            throw new ArgumentException("Duration must be greater zero.");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentNullException("Description cannot be null or empty.");

        description = description.Replace(" ", "\n");

        return await _adminRepository.AddTypePass(gymPassName, price, durationPass, description, cancellationToken);
    }

    public async Task<TrainerCourse> AddNewTrainerCourse(string courseName, string duration, string description,
        int coachId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(courseName))
            throw new ArgumentNullException("Course name cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(duration))
            throw new ArgumentNullException("Duration cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentNullException("Description cannot be null or empty.");
        return await _adminRepository.AddTrainerCourse(courseName, duration, description, coachId, cancellationToken);
    }

    public async Task<bool> DeleteTrainingPlan(int trainingPlanId, CancellationToken cancellationToken = default)
    {
        return await _adminRepository.RemoveTrainingPlan(trainingPlanId, cancellationToken);
    }

    public async Task<bool> DeleteFoodEbook(int foodEbookId, CancellationToken cancellationToken = default)
    {
        return await _adminRepository.RemoveFoodEbook(foodEbookId, cancellationToken);
    }
}