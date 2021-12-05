using Domain.Common;

namespace Domain.Entities
{
    public class ProductType : AuditableBaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
