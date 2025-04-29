using Business.Interfaces;
using GymRats.Data.Entities;
using GymRats.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace GymRats.Business.Services;


public class PersonServices : IPersonServices
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<PersonServices> _logger;

    public PersonServices(
        IPersonRepository personRepository,
        ILogger<PersonServices> logger)
    {
        _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Person?> GetPersonByCoachIdAsync(int coachId, CancellationToken cancellationToken = default)
    {
        try
        {
            if (!await _personRepository.CoachExistsAsync(coachId, cancellationToken))
            {
                _logger.LogWarning("Coach with ID {CoachId} not found", coachId);
                return null;
            }

            var person = await _personRepository.GetPersonByCoachIdAsync(coachId, cancellationToken);
            
            if (person == null)
            {
                _logger.LogWarning("Person not found for coach ID {CoachId}", coachId);
            }

            return person;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving person for coach ID {CoachId}", coachId);
            throw;
        }
    }
}