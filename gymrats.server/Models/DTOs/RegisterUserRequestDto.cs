using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gymrats.server.Models.DTOs
{
    public class RegisterUserRequestDto
    {
        [Required(ErrorMessage = "Required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Required.")]
        public string Haslo { get; set; } = null!;

        //[Key]
        //public int IdOsoba { get; set; }
        //[Key]
        //public int IdUzytkownika { get; set; }

        //[Required(ErrorMessage = "Required.")]
        //public string Imie { get; set; } = null!;

        //[Required(ErrorMessage = "Required.")]
        //public string Nazwisko { get; set; } = null!;

        //[Required(ErrorMessage = "Required.")]
        //public DateTime DataUrodzenia { get; set; }

        //[Required(ErrorMessage = "Required.")]
        //[EmailAddress(ErrorMessage = "Invalid email address.")]
        //public string Email { get; set; } = null!;

        //[Required(ErrorMessage = "Required.")]
        //public string Haslo { get; set; } = null!;

        //public string Adres { get; set; } = null!;

        //public string NrTel { get; set; } = null!;

        //public string Plec { get; set; } = null!;

        //public float Bmi { get; set; }

        //public float Waga { get; set; }

        //public int Wzrost { get; set; }
    }
}
