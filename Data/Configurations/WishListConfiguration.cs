using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class WishListConfiguration : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            builder.ToTable("WishLists");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.User).WithMany(x => x.WishLists)
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Product).WithMany(x => x.WishLists)
                .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
