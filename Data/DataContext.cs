using Data.Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new ConditionConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductPhotoConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());
            modelBuilder.ApplyConfiguration(new ShoppingCartConfiguration());
            modelBuilder.ApplyConfiguration(new SubBrandConfiguration());
            modelBuilder.ApplyConfiguration(new UserAddressConfiguration());
            modelBuilder.ApplyConfiguration(new WishListConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserLoginTokenConfiguration());
        }

        public DbSet<Brand>? Brands { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<Condition>? Conditions { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderDetail>? OrderDetails { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<ProductPhoto>? ProductPhotos { get; set; }
        public DbSet<ProductType>? ProductTypes { get; set; }
        public DbSet<Rating>? Ratings { get; set; }
        public DbSet<ShoppingCart>? ShoppingCarts { get; set; }
        public DbSet<SubBrand>? SubBrands { get; set; }
        public DbSet<UserAddress>? UserAddresses { get; set; }
        public DbSet<WishList>? WishLists { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<UserRole>? UserRoles { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<UserLoginToken>? UserLoginTokens { get; set; }
    }
}
