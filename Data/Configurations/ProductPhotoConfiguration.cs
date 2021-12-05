using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ProductPhotoConfiguration : IEntityTypeConfiguration<ProductPhoto>
    {
        public void Configure(EntityTypeBuilder<ProductPhoto> builder)
        {
            builder.ToTable("ProductPhotos");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.Product).WithMany(x => x.ProductPhotos)
                .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.ProductPhotoUrl).IsRequired().HasColumnType("nvarchar(255)");
            builder.Property(x => x.ProductPhotoId).IsRequired().HasColumnType("nvarchar(200)");
            builder.Property(x => x.Caption).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.LastModifiedAt).IsRequired(false);
            builder.Property(x => x.LastModifiedBy).IsRequired(false);
        }
    }
}
