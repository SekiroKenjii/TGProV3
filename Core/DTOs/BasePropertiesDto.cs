using Core.DTOs.Auditable;

namespace Core.DTOs
{
    public abstract class BasePropertiesDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public abstract class BaseAuditableDto : BasePropertiesDto
    {
        public CreatedByDto? CreatedInfo { get; set; }
        public LastModifiedByDto? LastModifiedInfo { get; set; }
    }
}
