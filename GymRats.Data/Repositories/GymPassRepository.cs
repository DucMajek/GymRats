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

        public async Task<IReadOnlyList<TypKarnetu>> GetAllGymPass(CancellationToken cancellationToken = default)
        {
            return await _context.TypKarnetus.ToListAsync(cancellationToken);
        }

        public async Task<Karnet?> GetGymPassByPersonId(int userId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.Karnets
                    .Where(k => k.UzytkownikIdUzytkownik == userId)
                    .Select(k => new Karnet
                    {
                        UzytkownikIdUzytkownik = k.UzytkownikIdUzytkownik,
                        StartKarnetu = k.StartKarnetu,
                        KoniecUmowy = k.KoniecUmowy,
                        TypKarnetuIdTypKarnetu = k.TypKarnetuIdTypKarnetu,
                        TypKarnetuIdTypKarnetuNavigation = k.TypKarnetuIdTypKarnetuNavigation
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