using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.ToTable("ShoppingCarts");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.User).WithMany(x => x.ShoppingCarts)
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Product).WithMany(x => x.ShoppingCarts)
                .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Quantity).IsRequired().HasDefaultValue(0);
        }
    }
}
