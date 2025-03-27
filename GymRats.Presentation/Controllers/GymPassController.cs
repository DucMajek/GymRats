using GymRats.Business.Interfaces;
using GymRats.Data.Entities;
using GymRats.Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GymRats.Presentation.Controllers
{
    public class GymPassController : ControllerBase
    {
        private readonly IGymPassServices _gymPassServices;
        private readonly ILogger<GymPassController> _logger;

        public GymPassController(
            IGymPassServices gymPassServices,
            ILogger<GymPassController> logger)
        {
            _gymPassServices = gymPassServices;
            _logger = logger;
        }

        [HttpGet("/categories")]
        public async Task<ActionResult<IReadOnlyList<TypKarnetu>>> GetGymPassCategories(CancellationToken cancellationToken = default)
        {
            try
            {
                var gymPasses = await _gymPassServices.AvailableGymPass(cancellationToken);
                
                if (!gymPasses.Any())
                {
                    return NoContent();
                }

                return Ok(gymPasses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving gym pass categories");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }
        
        [HttpGet("/membership/{userId}")]
        public async Task<ActionResult<GymPassResponse>> GetUserGymPass(int userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var gymPass = await _gymPassServices.UserGymPass(userId, cancellationToken);
                
                if (gymPass == null)
                {
                    return NotFound($"Gym pass not found for user {userId}");
                }

                return Ok(gymPass);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving gym pass for user {UserId}", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }
    }
}