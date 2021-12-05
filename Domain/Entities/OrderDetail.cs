using Domain.Common;

namespace Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public Guid? OrderId { get; set; }
        public virtual Order? Order { get; set; }

        public Guid? ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public int Quantity { get; set; }
    }
}
