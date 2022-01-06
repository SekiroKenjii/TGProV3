using Core.DTOs.Brand;
using Core.DTOs.Category;

namespace Core.DTOs.SubBrand
{
    public class SubBrandDto : BaseAuditableDto
    {
        public Guid Id { get; set; }
        public CompactCategoryDto? Category { get; set; }
        public CompactBrandDto? Brand { get; set; }
    }

    public class CompactSubBrandDto : BasePropertiesDto
    {
        public CompactCategoryDto? Category { get; set; }
        public CompactBrandDto? Brand { get; set; }
    }
}
