using System.ComponentModel.DataAnnotations;

namespace gymrats.server.Models.DTOs
{
    public class LoginRequestDto
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
