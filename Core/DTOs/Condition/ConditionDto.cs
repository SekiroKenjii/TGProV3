namespace Core.DTOs.Condition
{
    public class ConditionDto : BaseAuditableDto
    {
        public Guid Id { get; set; }
    }
    public class CompactConditionDto : BasePropertiesDto
    {
    }
}
