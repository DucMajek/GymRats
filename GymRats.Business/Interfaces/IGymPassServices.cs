using GymRats.Data.Entities;

namespace GymRats.Business.Interfaces;

public interface IGymPassServices
{
    Task<IReadOnlyList<TypePass>> AvailableGymPass(CancellationToken cancellationToken = default);
}