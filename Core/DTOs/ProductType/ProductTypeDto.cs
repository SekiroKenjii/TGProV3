namespace Core.DTOs.ProductType
{
    public class ProductTypeDto : BaseAuditableDto
    {
        public Guid Id { get; set; }
    }

    public class CompactProductTypeDto : BasePropertiesDto { }
}
