using Domain.Common;

namespace Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }

        public Guid RoleId { get; set; }
        public virtual Role? Role { get; set; }
    }
}
