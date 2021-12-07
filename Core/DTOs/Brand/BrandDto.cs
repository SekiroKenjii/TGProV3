using Core.DTOs.Auditable;

namespace Core.DTOs.Brand
{
    public class BrandDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? LogoUrl { get; set; }
        public string? LogoId { get; set; }
        public string? Description { get; set; }
        public CreatedByDto? CreatedInfo { get; set; }
        public LastModifiedByDto? LastModifiedInfo { get; set; }
    }
}
