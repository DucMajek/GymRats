using gymrats.server.Models;
using gymrats.server.Repositories;

namespace gymrats.server.Services;

public interface IPersonServices
{
    Task<Osoba> getOsobaById(int id);
}
public class PersonServices : IPersonServices
{
    private readonly IPersonRepository _personRepository;

    public PersonServices(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Osoba> getOsobaById(int id)
    {
        var personExists = await _personRepository.coachExists(id);
        if (!personExists)
            return null;
        return await _personRepository.getOsobaById(id);
    }
}