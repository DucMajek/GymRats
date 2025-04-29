using Business.Interfaces;
using GymRats.Business.Interfaces;
using GymRats.Data.Entities;
using GymRats.Data.Interfaces;
using GymRats.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace GymRats.Business.Services;

public class GymPassServices : IGymPassServices
{
    private readonly IGymPassRepository _gymPassRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<GymPassServices> _logger;

    public GymPassServices(
        IGymPassRepository gymPassRepository,
        IUserRepository userRepository,
        ILogger<GymPassServices> logger)
    {
        _gymPassRepository = gymPassRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<IReadOnlyList<TypePass>> AvailableGymPass(CancellationToken cancellationToken = default)
    {
        try
        {
            var gymPass = await _gymPassRepository.GetAllGymPass(cancellationToken);
            return gymPass ?? Array.Empty<TypePass>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving available gym passes");
            return Array.Empty<TypePass>();
        }
    }

    public async Task<UserPass?> UserGymPass(string email, CancellationToken cancellationToken = default)
    {
        try
        {   
            var userId = _userRepository.GetUser(email);
            return await _gymPassRepository.GetGymPassByPersonId(userId.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving gym pass for user {Email}", email);
            return null;
        }
    }
}