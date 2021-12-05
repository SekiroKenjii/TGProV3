using Core.Enums;
using Domain.Common;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public string? AvatarId { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public Gender Gender { get; set; }
        public bool IsBlocked { get; set; }

        public virtual ICollection<UserRole>? UserRoles { get; set; } = new List<UserRole>();
        public virtual ICollection<UserLoginToken>? UserLoginTokens { get; set; } = new List<UserLoginToken>();
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<UserAddress>? UserAddresses { get; set; }
        public virtual ICollection<ShoppingCart>? ShoppingCarts { get; set; }
        public virtual ICollection<WishList>? WishLists { get; set; }
        public virtual ICollection<Rating>? Ratings { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
