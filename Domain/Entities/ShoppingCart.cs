using Domain.Common;

namespace Domain.Entities
{
    public class ShoppingCart : BaseEntity
    {
        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }

        public Guid? ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public int Quantity { get; set; }
    }
}
