using GymRats.Data.Entities;

namespace Business.Interfaces;

public interface IPersonServices
{
    Task<Osoba?> GetPersonByCoachIdAsync(int coachId, CancellationToken cancellationToken = default);
}