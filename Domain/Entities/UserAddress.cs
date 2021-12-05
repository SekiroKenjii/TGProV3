using Domain.Common;

namespace Domain.Entities
{
    public class UserAddress : BaseEntity
    {
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public bool IsDefault { get; set; }

        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
