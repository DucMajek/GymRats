using GymRats.Business.Interfaces;
using GymRats.Data.Entities;
using GymRats.Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace GymRats.Business.Services;

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



    public async Task<IReadOnlyList<TypePass>> AvailableGymPass(CancellationToken cancellationToken = default)
    {
        try
        {
            var gymPass = await _gymPassRepository.GetAllGymPass(cancellationToken);
            return gymPass;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving available gym passes");
            return Array.Empty<TypePass>();
        }
    }
    
}