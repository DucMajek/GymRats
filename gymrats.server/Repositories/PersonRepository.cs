using gymrats.server.Models;
using Microsoft.EntityFrameworkCore;

namespace gymrats.server.Repositories;

public interface IPersonRepository
{
    Task<bool> CoachExistsAsync(int coachId, CancellationToken cancellationToken = default);
    Task<Osoba?> GetPersonByCoachIdAsync(int coachId, CancellationToken cancellationToken = default);
}

public class PersonRepository : IPersonRepository
{
    private readonly GymRatsContext _context;
    private readonly ILogger<PersonRepository> _logger;

    public PersonRepository(
        GymRatsContext context,
        ILogger<PersonRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> CoachExistsAsync(int coachId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Treners
                .AnyAsync(e => e.IdTrenera == coachId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if coach with ID {CoachId} exists", coachId);
            throw;
        }
    }

    public async Task<Osoba?> GetPersonByCoachIdAsync(int coachId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Osobas
                .Include(o => o.Treners)
                .FirstOrDefaultAsync(e => e.Treners.Any(t => t.IdTrenera == coachId), 
                    cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving person for coach ID {CoachId}", coachId);
            throw;
        }
    }
}