using GymRats.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymRats.Data.Repositories;

public interface IPersonRepository
{
    Task<bool> CoachExistsAsync(int coachId, CancellationToken cancellationToken = default);
    Task<Person?> GetPersonByCoachIdAsync(int coachId, CancellationToken cancellationToken = default);
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
            return await _context.Coaches
                .AnyAsync(e => e.IdCoach == coachId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if coach with ID {CoachId} exists", coachId);
            throw;
        }
    }

    public async Task<Person?> GetPersonByCoachIdAsync(int coachId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.People
                .Include(o => o.Coach)
                .FirstOrDefaultAsync(o => o.Coach != null && o.Coach.IdCoach == coachId, 
                    cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving person for coach ID {CoachId}", coachId);
            throw;
        }
    }
}