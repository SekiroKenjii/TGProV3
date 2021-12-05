namespace Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        public virtual Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public Guid? LastModifiedBy { get; set; }
    }
}
