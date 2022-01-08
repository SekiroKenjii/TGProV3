using Core.DTOs.Auditable;

namespace Core.DTOs.Product
{
    public class ProductPhotoDto
    {
        public Guid Id { get; set; }
        public string? ProductPhotoUrl { get; set; }
        public string? ProductPhotoId { get; set; }
        public string? Caption { get; set; }
        public int SortOrder { get; set; }
        public CreatedByDto? CreatedInfo { get; set; }
        public LastModifiedByDto? LastModifiedInfo { get; set; }
    }
    public class CompactProductPhotoDto
    {
        public string? ProductPhotoUrl { get; set; }
        public string? Caption { get; set; }
        public int SortOrder { get; set; }
    }
}
