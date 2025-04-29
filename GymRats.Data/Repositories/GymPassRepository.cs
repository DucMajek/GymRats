using GymRats.Data.Entities;
using GymRats.Data.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymRats.Data.Repositories
{
    public class GymPassRepository : IGymPassRepository
    {
        private readonly GymRatsContext _context;
        private readonly ILogger<GymPassRepository> _logger;

        public GymPassRepository(GymRatsContext context, ILogger<GymPassRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IReadOnlyList<TypePass>> GetAllGymPass(CancellationToken cancellationToken = default)
        {
            return await _context.TypePasses.ToListAsync(cancellationToken);
        }

        public async Task<UserPass?> GetGymPassByPersonId(int userId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.UserPasses
                    .Where(k => k.IdUser == userId)
                    .Select(k => new UserPass
                    {
                        IdUser = k.IdUser,
                        DateStart = k.DateStart,
                        DateEnd = k.DateEnd,
                        IdTypePass = k.IdTypePass,
                        IdTypePassNavigation = k.IdTypePassNavigation
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving gym pass for user ID {UserId}", userId);
                throw;
            }
        }
    }
}