namespace GymRats.Presentation.DTOs
{
    public class RegisterUserResponse
    {
        
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? UserId { get; set; }
        public string? Email { get; set; }
        public DateTime? RegistrationDate { get; set; }
    
        // Dodatkowe informacje, które mogą być przydatne dla klienta
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
