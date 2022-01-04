namespace Core.DTOs.SubBrand
{
    public class AddSubBrandDto : BasePropertiesDto
    {
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
    }
}