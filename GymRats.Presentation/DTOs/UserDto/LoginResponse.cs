using GymRats.Data.Entities;

namespace GymRats.Presentation.DTOs
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; } = null!;
        public string Message { get; set; } = null!;
     
    }
}
