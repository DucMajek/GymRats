
using GymRats.Data;
using GymRats.Data.Entities;

namespace GymRats.Business.Interfaces;

public interface IGymPassServices
{
    Task<UserPass?> UserGymPass(string email, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TypePass>> AvailableGymPass(CancellationToken cancellationToken = default);
}