using GymRats.Business.Exceptions;
using GymRats.Business.Interfaces;
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
    public async Task<ActionResult> NewFoodBook(string trainingPlanName, IFormFile trainingPlanFile,
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
    public async Task<ActionResult> NewFoodBook(int gymPassId, int newPassPrice,
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

    [HttpPost("admin/newTrainerCourse/{courseName}/{duration}/{description}/{coachId}")]
    public async Task<ActionResult> NewGymPass(string courseName, string duration, string description,
        int coachId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var newTrainerCourse =
                await _adminService.AddNewTrainerCourse(courseName, duration, description, coachId, cancellationToken);
            return Ok(newTrainerCourse);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}