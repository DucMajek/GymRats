using GymRats.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Data.Repositories;

public interface ITrainingCourseRepository
{
    public Task<List<KursTrenera>> GetAllTrainingCourses();
}

public class TrainingCourseRepository : ITrainingCourseRepository
{
    private readonly GymRatsContext _context;
    
    public TrainingCourseRepository( GymRatsContext context)
    {
        _context = context;
    }
    public async Task<List<KursTrenera>> GetAllTrainingCourses()
    {
        return await _context.KursTreneras.Select(e => new KursTrenera
        {
            IdKursu = e.IdKursu,
            Nazwa = e.Nazwa,
            CzasTrwania = e.CzasTrwania,
            TrenerIdTrenera = e.TrenerIdTrenera,
            TrenerIdTreneraNavigation = e.TrenerIdTreneraNavigation,
        }).ToListAsync();
    }
}