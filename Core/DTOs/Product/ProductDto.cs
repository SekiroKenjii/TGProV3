using Core.DTOs.Category;
using Core.DTOs.Condition;
using Core.DTOs.ProductType;
using Core.DTOs.SubBrand;

namespace Core.DTOs.Product
{
    public class ProductDto : BaseAuditableDto
    {
        public Guid Id { get; set; }
        public string? SerialNumber { get; set; }
        public string? Specification { get; set; }
        public string? Warranty { get; set; }
        public double Price { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public bool Discontinued { get; set; }
        public CategoryDto? Category { get; set; }
        public ConditionDto? Condition { get; set; }
        public SubBrandDto? SubBrand { get; set; }
        public ProductTypeDto? ProductType { get; set; }
        //public List<ProductPhotoDto>? ProductPhotos { get; set; } = new();
    }
    public class CompactProductDto : BasePropertiesDto
    {
        public string? SerialNumber { get; set; }
        public string? Specification { get; set; }
        public string? Warranty { get; set; }
        public double Price { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public bool Discontinued { get; set; }
        public CompactCategoryDto? Category { get; set; }
        public CompactConditionDto? Condition { get; set; }
        public CompactSubBrandDto? SubBrand { get; set; }
        public CompactProductTypeDto? ProductType { get; set; }
    }
}
