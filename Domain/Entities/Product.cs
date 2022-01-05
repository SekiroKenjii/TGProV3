using Domain.Common;

namespace Domain.Entities
{
    public class Product : AuditableBaseEntity
    {
        public string? SerialNumber { get; set; }
        public string? Name { get; set; }
        public string? Specification { get; set; }
        public string? Description { get; set; }
        public string? Warranty { get; set; }
        public double Price { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public bool Discontinued { get; set; }

        public Guid? CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public Guid? ConditionId { get; set; }
        public virtual Condition? Condition { get; set; }

        public Guid? SubBrandId { get; set; }
        public virtual SubBrand? SubBrand { get; set; }

        public Guid? ProductTypeId { get; set; }
        public virtual ProductType? ProductType { get; set; }

        public virtual ICollection<ProductPhoto>? ProductPhotos { get; set; } = new List<ProductPhoto>();
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
        public virtual ICollection<ShoppingCart>? ShoppingCarts { get; set; }
        public virtual ICollection<WishList>? WishLists { get; set; }
        public virtual ICollection<Rating>? Ratings { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
