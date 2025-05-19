using GymRats.Data.Entities;
using GymRats.Data.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymRats.Data.Repositories
{
    public class GymPassRepository : IGymPassRepository
    {
        private readonly GymRatsContext _context;

        public GymPassRepository(GymRatsContext context, ILogger<GymPassRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IReadOnlyList<TypePass>> GetAllGymPass(CancellationToken cancellationToken = default)
        {
            return await _context.TypePasses.ToListAsync(cancellationToken);
        }
        
    }
}