using GymRats.Data.Entities;
using GymRats.Data.Repositories;
using GymRats.Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GymRats.Presentation.Controllers;

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