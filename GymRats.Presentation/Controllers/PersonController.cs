using Business.Interfaces;
using GymRats.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GymRats.Presentation.Controllers;

public class PersonController : ControllerBase
{
    private readonly IPersonServices _personServices;
    private readonly ILogger<PersonController> _logger;

    public PersonController(
        IPersonServices personServices,
        ILogger<PersonController> logger)
    {
        _personServices = personServices ?? throw new ArgumentNullException(nameof(personServices));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet("/coaches/{coachId}")]
    public async Task<ActionResult<Osoba>> GetCoachPerson(
        [FromRoute] int coachId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var person = await _personServices.GetPersonByCoachIdAsync(coachId, cancellationToken);
            
            if (person == null)
            {
                _logger.LogWarning("Person not found for coach ID {CoachId}", coachId);
                return NotFound($"Person not found for coach ID {coachId}");
            }

            return Ok(person);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving person for coach ID {CoachId}", coachId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
        }
    }
}