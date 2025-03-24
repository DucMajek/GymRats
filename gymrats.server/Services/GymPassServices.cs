using gymrats.server.Models;
using gymrats.server.Repositories;
using Microsoft.Extensions.Logging;

namespace gymrats.server.Services;

public interface IGymPassServices
{
    Task<Karnet?> UserGymPass(int userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TypKarnetu>> AvailableGymPass(CancellationToken cancellationToken = default);
}

public class GymPassServices : IGymPassServices
{
    private readonly IGymPassRepository _gymPassRepository;
    private readonly ILogger<GymPassServices> _logger;

    public GymPassServices(
        IGymPassRepository gymPassRepository,
        ILogger<GymPassServices> logger)
    {
        _gymPassRepository = gymPassRepository;
        _logger = logger;
    }

    public async Task<IReadOnlyList<TypKarnetu>> AvailableGymPass(CancellationToken cancellationToken = default)
    {
        try
        {
            var gymPass = await _gymPassRepository.GetAllGymPass(cancellationToken);
            return gymPass ?? Array.Empty<TypKarnetu>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving available gym passes");
            return Array.Empty<TypKarnetu>();
        }
    }

    public async Task<Karnet?> UserGymPass(int userId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _gymPassRepository.GetGymPassByPersonId(userId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving gym pass for user {UserId}", userId);
            return null;
        }
    }
}