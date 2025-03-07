using gymrats.server.Services;
using Microsoft.AspNetCore.Mvc;

namespace gymrats.server.Controllers;

public class PersonController : ControllerBase
{
    private readonly IPersonServices _personServices;

    public PersonController(IPersonServices personServices)
    {
        _personServices = personServices;
    }

    [HttpGet("/Coach/{id}")]
    public async Task<IActionResult> GetPerson(int id)
    {
        var person = await _personServices.getOsobaById(id);
        if(person == null)
            return BadRequest("Person doesn't exist");
        return Ok(person);

    }

}