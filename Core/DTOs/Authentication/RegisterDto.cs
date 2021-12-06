using Domain.Enums;

namespace Core.DTOs.Authentication
{
    public class RegisterDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string? Password { get; set; }

    }
}
