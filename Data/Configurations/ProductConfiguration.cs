using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.SubBrand).WithMany(x => x.Products)
                .HasForeignKey(x => x.SubBrandId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Category).WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Condition).WithMany(x => x.Products)
                .HasForeignKey(x => x.ConditionId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.ProductType).WithMany(x => x.Products)
                .HasForeignKey(x => x.ProductTypeId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.SerialNumber).IsRequired().HasColumnType("varchar(500)");
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.Specification).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(x => x.Description).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(x => x.Warranty).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.Price).IsRequired().HasDefaultValue(0.00);
            builder.Property(x => x.UnitsInStock).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.UnitsOnOrder).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.Discontinued).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.LastModifiedAt).IsRequired(false);
            builder.Property(x => x.LastModifiedBy).IsRequired(false);
        }
    }
}
