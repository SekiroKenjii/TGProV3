using Domain.Common;

namespace Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string? CommentContent { get; set; }
        public DateTime CommentedAt { get; set; }

        public Guid? ParentId { get; set; }
        public virtual Comment? ParentComment { get; set; }

        public Guid? ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<Comment>? ChildComments { get; set; }
    }
}
