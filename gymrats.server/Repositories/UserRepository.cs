using gymrats.server.Models;
using gymrats.server.Models.DTOs;
using gymrats.server.Services;
using Microsoft.EntityFrameworkCore;


namespace gymrats.server.Repositories
{
    public interface IUserRepository 
    {
        Task<bool> UserExist(LoginRequestDto login);
        Task<bool> EmailExist(RegisterUserRequestDto newUser);
        Task<Uzytkownik> AddNewUser(RegisterUserRequestDto newUser);
        
    }
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly GymRatsContext _context;
        private readonly IPasswordHasher _passwordHasher;
        public UserRepository(IConfiguration configuration, GymRatsContext context, IPasswordHasher passwordHasher)
        {
            _configuration = configuration;
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Uzytkownik> AddNewUser(RegisterUserRequestDto newUser)
        {
            var person = new Osoba
            {
                Imie = newUser.Imie,
                Nazwisko = newUser.Nazwisko,
                DataUrodzenia = null,
                Adres = string.Empty,
                NrTel = string.Empty,
                Plec = string.Empty,
                Bmi = 0,
                Waga = 0,
                Wzrost = 0,

            };
            await _context.Osobas.AddAsync(person);
            await _context.SaveChangesAsync();

            var user = new Uzytkownik
            {
                Email = newUser.Email,
                Haslo = _passwordHasher.HashPassword(newUser.Password),
                OsobaIdOsoba = person.IdOsoba
            };
            await _context.Uzytkowniks.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> EmailExist(RegisterUserRequestDto newUser)
        {
            return await _context.Uzytkowniks.AnyAsync(e => e.Email == newUser.Email);
        }

        public async Task<bool> UserExist(LoginRequestDto login) { 
            var user = await _context.Uzytkowniks.FirstOrDefaultAsync(e => e.Email == login.Email);
            
            if (user == null)
            {
                return false;
            }

            bool isValidPassword = _passwordHasher.VerifyPassword(login.Password, user.Haslo);

            return isValidPassword;
        }

    }
}
