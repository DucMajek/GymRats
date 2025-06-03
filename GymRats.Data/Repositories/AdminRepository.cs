using GymRats.Data.Entities;
using GymRats.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymRats.Data.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly GymRatsContext _context;

    public AdminRepository(
        GymRatsContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> IsAdmin(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .AnyAsync(e => e.IdUser == userId && e.IdRole == 3, cancellationToken);
    }


    public async Task<FoodEbook> AddFoodEbook(string calories, string dietType, byte[] EbookFile,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        var newFoodEbook = new FoodEbook()
        {
            Calories = calories,
            DietType = dietType,
            EbookFile = EbookFile
        };
        await _context.FoodEbooks.AddAsync(newFoodEbook, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return newFoodEbook;
    }

    public async Task<TrainingPlan> AddTrainingPlan(string trainingPlanName, byte[] TrainingPlanFile,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        var newTrainingPlan = new TrainingPlan()
        {
            TrainingPlanName = trainingPlanName,
            TrainingPlanFile = TrainingPlanFile
        };
        await _context.TrainingPlans.AddAsync(newTrainingPlan, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return newTrainingPlan;
    }

    public async Task<TypePass> UpdateGymPassPrice(int gymPassId, int newPrice,
        CancellationToken cancellationToken = default)
    {
        var gymPass = await _context.TypePasses
            .Where(e => e.IdTypePass == gymPassId)
            .FirstOrDefaultAsync(cancellationToken);

        gymPass.Price = newPrice;
        await _context.SaveChangesAsync(cancellationToken);
        return gymPass;
    }

    public async Task<TypePass> AddTypePass(string gymPassName, int price, int durationPass, string description,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        var newGymPass = new TypePass()
        {
            GymPassName = gymPassName,
            Price = price,
            DurationPass = durationPass,
            Description = description
        };
        await _context.TypePasses.AddAsync(newGymPass, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return newGymPass;
    }

    public async Task<TrainerCourse> AddTrainerCourse(string courseName, string duration, string description,
        int coachId,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        var newTrainerCourse = new TrainerCourse()
        {
            CourseName = courseName,
            Duration = duration,
            Description = description,
            IdCoach = coachId
        };
        await _context.TrainerCourses.AddAsync(newTrainerCourse, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return newTrainerCourse;
    }
}