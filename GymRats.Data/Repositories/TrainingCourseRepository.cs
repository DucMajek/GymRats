using GymRats.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Data.Repositories;

public interface ITrainingCourseRepository
{
    public Task<List<TrainerCourse>> GetAllTrainingCourses();
}

public class TrainingCourseRepository : ITrainingCourseRepository
{
    private readonly GymRatsContext _context;
    
    public TrainingCourseRepository( GymRatsContext context)
    {
        _context = context;
    }
    public async Task<List<TrainerCourse>> GetAllTrainingCourses()
    {
        return await _context.TrainerCourses.Select(e => new TrainerCourse
        {
            IdCourse = e.IdCourse,
            CourseName = e.CourseName,
            Duration = e.Duration,
            Description = e.Description,
            IdCoach = e.IdCoach,
            IdCoachNavigation = e.IdCoachNavigation,
        }).ToListAsync();
    }
}