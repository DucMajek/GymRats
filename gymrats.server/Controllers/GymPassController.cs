using gymrats.server.Models;
using gymrats.server.Repositories;
using gymrats.server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace gymrats.server.Controllers
{
    public class GymPassController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IGymPassRepository _gymPassRepository;

        public GymPassController(IConfiguration configuration, IGymPassRepository gymPassRepository)
        {
            _configuration = configuration;
            _gymPassRepository = gymPassRepository;
        }

        [HttpGet("/GymPassCategory")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _gymPassRepository.GetAllGymPass());
        }

        
    }
}
