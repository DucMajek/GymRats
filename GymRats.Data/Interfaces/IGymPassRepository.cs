using GymRats.Data.Entities;

namespace GymRats.Data.Interfaces;

public interface IGymPassRepository
{
    public Task<IReadOnlyList<TypePass>> GetAllGymPass(CancellationToken cancellationToken = default);

}