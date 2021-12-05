using Domain.Common;

namespace Domain.Entities
{
    public class SubBrand : AuditableBaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public Guid? BrandId { get; set; }
        public virtual Brand? Brand { get; set; }

        public Guid? CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
