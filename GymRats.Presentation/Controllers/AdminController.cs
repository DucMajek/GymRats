using GymRats.Business.Exceptions;
using GymRats.Business.Interfaces;
using GymRats.Presentation.DTOs.AdminDto;
using Microsoft.AspNetCore.Mvc;


namespace GymRats.Presentation.Controllers;

public class AdminController : ControllerBase
{
    private readonly IAdminServices _adminService;
    private readonly ILogger<AdminController> _logger;

    public AdminController(
        IAdminServices adminService,
        ILogger<AdminController> logger)
    {
        _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost("admin/newFoodBook/{calories}/{dietType}")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult> NewFoodBook(string calories, string dietType, IFormFile EbookFile,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var newFoodBook = await _adminService.AddNewFoodEbook(calories, dietType, EbookFile, cancellationToken);
            return Ok(newFoodBook);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidFileFormatException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("admin/newTrainigPlan/{trainingPlanName}")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult> NewTrainingPlan(string trainingPlanName, IFormFile trainingPlanFile,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var newTrainingPlan =
                await _adminService.AddNewTrainingPlan(trainingPlanName, trainingPlanFile, cancellationToken);
            return Ok(newTrainingPlan);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("admin/updateGymPassPrice/{gymPassId}/{newPassPrice}")]
    public async Task<ActionResult> NewGymPassPrice(int gymPassId, int newPassPrice,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var gymPass = await _adminService.ChangeGymPassPrice(gymPassId, newPassPrice, cancellationToken);
            return Ok(gymPass);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("admin/NewGymPass/{gymPassName}/{price}/{durationPass}/{description}")]
    public async Task<ActionResult> NewGymPass(string gymPassName, int price, int durationPass, string description,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var newGymPass =
                await _adminService.AddNewTypePass(gymPassName, price, durationPass, description, cancellationToken);
            return Ok(newGymPass);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("admin/newTrainerCourse")]
    
    public async Task<ActionResult> NewTrainerCourse([FromBody] NewTrainerCourseDto dto,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var newTrainerCourse = await _adminService.AddNewTrainerCourse(
                dto.CourseName,
                dto.Duration,
                dto.Description,
                dto.CoachId,
                cancellationToken);

            return Ok(newTrainerCourse);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error adding trainer course");
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("admin/deleteTrainingPlan/{trainingPlanId}/")]
    public async Task<ActionResult> DeleteTrainingPlan(int trainingPlanId,
        CancellationToken cancellationToken = default)
    {
        var trainingPlan = await _adminService.DeleteTrainingPlan(trainingPlanId, cancellationToken);
        return Ok(trainingPlan);
    }
    
    [HttpDelete("admin/deleteFoodEbook/{foodEbookId}/")]
    public async Task<ActionResult> DeleteFoodEbook(int foodEbookId,
        CancellationToken cancellationToken = default)
    {
        var foodEbook = await _adminService.DeleteFoodEbook(foodEbookId, cancellationToken);
        return Ok(foodEbook);
    }
}