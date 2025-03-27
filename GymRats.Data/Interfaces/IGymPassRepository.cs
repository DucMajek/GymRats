using GymRats.Data.Entities;

namespace GymRats.Data.Interfaces;

public interface IGymPassRepository
{
    public Task<IReadOnlyList<TypKarnetu>> GetAllGymPass(CancellationToken cancellationToken = default);

    public Task<Karnet?> GetGymPassByPersonId(int gymPassId,
        CancellationToken cancellationToken = default);
}