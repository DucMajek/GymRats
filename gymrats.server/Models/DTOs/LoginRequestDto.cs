using System.ComponentModel.DataAnnotations;

namespace gymrats.server.Models.DTOs
{
    public class LoginRequestDto
    {

        [Required(ErrorMessage = "Required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Required.")]
        public string Haslo { get; set; } = null!;
    }
}
