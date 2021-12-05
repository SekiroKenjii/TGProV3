using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.Customer).WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.OrderedAt).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.OrderTotalOriginal).IsRequired();
            builder.Property(x => x.OrderTotal).IsRequired();
            builder.Property(x => x.PaymentStatus).IsRequired().HasColumnType("tinyint");
            builder.Property(x => x.OrderStatus).IsRequired().HasColumnType("tinyint");
            builder.Property(x => x.Note).IsRequired(false).HasColumnType("nvarchar(500)");
            builder.Property(x => x.ShipAddress).IsRequired().HasColumnType("nvarchar(255)");
        }
    }
}
