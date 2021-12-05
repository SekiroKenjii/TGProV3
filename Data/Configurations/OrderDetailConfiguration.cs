using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.Order).WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Product).WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Quantity).IsRequired().HasDefaultValue(1);
        }
    }
}
