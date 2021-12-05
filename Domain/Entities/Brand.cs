using Domain.Common;

namespace Domain.Entities
{
    public class Brand : AuditableBaseEntity
    {
        public string? Name { get; set; }
        public string? LogoUrl { get; set; }
        public string? LogoId { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<SubBrand>? SubBrands { get; set; }
    }
}
