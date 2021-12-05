using Domain.Common;

namespace Domain.Entities
{
    public class Rating : BaseEntity
    {
        public double RatingLevel { get; set; }
        public string? RatingContent { get; set; }
        public DateTime RatingAt { get; set; }

        public Guid? ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
