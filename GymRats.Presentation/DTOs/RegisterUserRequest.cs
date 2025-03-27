using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace GymRats.Presentation.DTOs
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "Required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", 
            ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Required.")]
        public string Password { get; set; } = null!;
        
        [Required(ErrorMessage = "Required.")]
        public string Imie { get; set; } = null!;

        [Required(ErrorMessage = "Required.")]
        public string Nazwisko { get; set; } = null!;
    }

    public class RegisterUserPersonalDataRequest
    {
        [Required(ErrorMessage = "Required.")]
        public string Imie { get; set; } = null!;

        [Required(ErrorMessage = "Required.")]
        public string Nazwisko { get; set; } = null!;
    }
}
