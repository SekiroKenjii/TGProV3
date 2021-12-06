namespace Core.DTOs.Auditable
{
    public class CreatedByDto
    {
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class LastModifiedByDto
    {
        public Guid? UserId { get; set; }
        public string? FullName { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
