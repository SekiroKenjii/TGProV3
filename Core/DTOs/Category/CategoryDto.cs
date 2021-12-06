using Core.DTOs.Auditable;

namespace Core.DTOs.Category
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public CreatedByDto? CreatedInfo { get; set; }
        public LastModifiedByDto? LastModifiedInfo { get; set; }
    }
}
