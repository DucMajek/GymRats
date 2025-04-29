using GymRats.Data.Entities;

namespace Business.Interfaces;

public interface IPersonServices
{
    Task<Person?> GetPersonByCoachIdAsync(int coachId, CancellationToken cancellationToken = default);
}