using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Ratings");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.User).WithMany(x => x.Ratings)
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Product).WithMany(x => x.Ratings)
                .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.RatingLevel).IsRequired().HasDefaultValue(0.0);
            builder.Property(x => x.RatingContent).IsRequired(false).HasColumnType("nvarchar(500)");
            builder.Property(x => x.RatingAt).IsRequired().HasDefaultValue(DateTime.UtcNow);
        }
    }
}
