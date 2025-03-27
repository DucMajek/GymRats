
using GymRats.Data.Entities;

namespace GymRats.Business.Interfaces;

public interface IGymPassServices
{
    Task<Karnet?> UserGymPass(int userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TypKarnetu>> AvailableGymPass(CancellationToken cancellationToken = default);
}