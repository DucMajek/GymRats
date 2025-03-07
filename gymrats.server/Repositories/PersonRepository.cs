using gymrats.server.Models;
using Microsoft.EntityFrameworkCore;
namespace gymrats.server.Repositories;

public interface IPersonRepository
{
    Task<bool> coachExists(int id);
    Task<Osoba> getOsobaById(int id);
}
public class PersonRepository : IPersonRepository
{
    private readonly GymRatsContext _context;
    public PersonRepository(GymRatsContext context)
    {
        _context = context;
    }
    public async Task<bool> coachExists(int id)
    {
        return await _context.Treners.AnyAsync(e => e.IdTrenera == id);
    }

    public async Task<Osoba> getOsobaById(int id)
    {
        return await _context.Osobas
            .Include(o => o.Treners)
            .FirstOrDefaultAsync(e => e.Treners.Any(t => t.IdTrenera == id));
    }
}