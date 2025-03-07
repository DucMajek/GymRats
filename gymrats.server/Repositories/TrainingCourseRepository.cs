using gymrats.server.Models;
using Microsoft.EntityFrameworkCore;

namespace gymrats.server.Repositories;

public interface ITrainingCourseRepository
{
    public Task<List<KursTrenera>> GetAllTrainingCourses();
}

public class TrainingCourseRepository : ITrainingCourseRepository
{
    private readonly IConfiguration _configuration;
    private readonly GymRatsContext _context;
    
    public TrainingCourseRepository(IConfiguration configuration, GymRatsContext context)
    {
        _configuration = configuration;
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