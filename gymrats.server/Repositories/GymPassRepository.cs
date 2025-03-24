using gymrats.server.Models;
using Microsoft.EntityFrameworkCore;

namespace gymrats.server.Repositories
{
    public interface IGymPassRepository 
    {
        public Task<IReadOnlyList<TypKarnetu>> GetAllGymPass(CancellationToken cancellationToken = default);
        public Task<Karnet?> GetGymPassByPersonId(int gymPassId,CancellationToken cancellationToken = default);
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

        public async Task<IReadOnlyList<TypKarnetu>> GetAllGymPass(CancellationToken cancellationToken = default)
        {
            return await _context.TypKarnetus.ToListAsync(cancellationToken);
        }

        public async Task<Karnet?> GetGymPassByPersonId(int userId, CancellationToken cancellationToken = default)
        {
            return await _context.Karnets
                .FirstOrDefaultAsync(e => e.UzytkownikIdUzytkownika == userId, cancellationToken);
        }
    }
}
