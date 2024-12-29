using gymrats.server.Models;
using Microsoft.EntityFrameworkCore;

namespace gymrats.server.Repositories
{
    public interface IGymPassRepository 
    {
        public Task<List<TypKarnetu>> GetAllGymPass();
    }
    public class GymPassRepository : IGymPassRepository 
    {
        private readonly GymRatsContext _context;
        private readonly IConfiguration _configuration;
        public GymPassRepository(IConfiguration configuration, GymRatsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<List<TypKarnetu>> GetAllGymPass()
        {
           return await _context.TypKarnetus.Select(e => new TypKarnetu
           {
               Nazwa = e.Nazwa,
               Cena = e.Cena,
               Opis = e.Opis

           }).ToListAsync();
        }
    }
}
