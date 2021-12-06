using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderedAt { get; set; }
        public double OrderTotalOriginal { get; set; }
        public double OrderTotal { get; set; }
        public string? Note { get; set; }
        public string? ShipAddress { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public Guid? ApprovedBy { get; set; }
        public Guid? PackedBy { get; set; }

        public Guid? CustomerId { get; set; }
        public virtual User? Customer { get; set; }

        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
