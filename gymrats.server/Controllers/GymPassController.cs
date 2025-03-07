using gymrats.server.Models;
using gymrats.server.Repositories;
using gymrats.server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace gymrats.server.Controllers
{
    public class GymPassController : ControllerBase
    {
        private readonly IGymPassRepository _gymPassRepository;

        public GymPassController(IGymPassRepository gymPassRepository)
        {
            _gymPassRepository = gymPassRepository;
        }

        [HttpGet("/GymPassCategory")]
        public async Task<IActionResult> GetAllGymPass()
        {
            var gymPass = await _gymPassRepository.GetAllGymPass();
            if(gymPass == null || !gymPass.Any())
                return BadRequest("Gym pass not exist");
            return Ok(gymPass);
        }

        
    }
}
