using gymrats.server.Repositories;
using gymrats.server.Services;
using Microsoft.AspNetCore.Mvc;

namespace gymrats.server.Controllers;

public class TrainingCourseController : ControllerBase
{
    private readonly ITrainingCourseRepository _trainingCourseRepository;

    public TrainingCourseController(ITrainingCourseRepository trainingCourseRepository)
    {
        _trainingCourseRepository = trainingCourseRepository;
    }

    [HttpGet("/Courses")]
    public async Task<IActionResult> GetAllCourses()
    {
        var courses = await _trainingCourseRepository.GetAllTrainingCourses();
        if(courses == null || !courses.Any())
            return BadRequest("No courses exist");
        return Ok(courses);

    }


}