using Domain.Enums;

namespace Core.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? FullName { get; set; }
        public string? Avatar { get; set; }
        public Gender Gender { get; set; }
        public bool IsBlocked { get; set; }
    }
    public class SimpleUserDto
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Avatar { get; set; }
    }
}
