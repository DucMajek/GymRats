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

        [Required(ErrorMessage = "Required.")] public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Required.")] public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Required.")] public string Surname { get; set; } = null!;
        [Required(ErrorMessage = "Required.")] public string Birthday { get; set; }
        [Required(ErrorMessage = "Required.")] public string PhoneNumber { get; set; } = null!;
        [Required(ErrorMessage = "Required.")] public string Gender { get; set; } = null!;
        [Required(ErrorMessage = "Required.")] public string Address { get; set; } = null!;
        [Required(ErrorMessage = "Required.")] public string FlatNumber { get; set; } = null!;

        [Required(ErrorMessage = "Required.")]
        [RegularExpression(@"^\d{2}-\d{3}$",
            ErrorMessage = "Please enter a valid zip code.")]
        public string ZipCode { get; set; } = null!;

        [Required(ErrorMessage = "Required.")] public string Place { get; set; } = null!;
    }

    /*public class RegisterUserPersonalDataRequest
    {
        [Required(ErrorMessage = "Required.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Required.")]
        public string Surname { get; set; } = null!;
    }*/
}