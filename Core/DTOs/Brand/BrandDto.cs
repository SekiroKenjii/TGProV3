namespace Core.DTOs.Brand
{
    public class BrandDto : BaseAuditableDto
    {
        public Guid Id { get; set; }
        public string? LogoUrl { get; set; }
        public string? LogoId { get; set; }
    }
    public class CompactBrandDto : BasePropertiesDto
    {
        public string? LogoUrl { get; set; }
    }
}
