using Domain.Common;

namespace Domain.Entities
{
    public class ProductPhoto : AuditableBaseEntity
    {
        public string? ProductPhotoUrl { get; set; }
        public string? ProductPhotoId { get; set; }
        public string? Caption { get; set; }
        public int SortOrder { get; set; }

        public Guid? ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}
