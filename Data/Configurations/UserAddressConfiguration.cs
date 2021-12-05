using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable("UserAddresses");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.User).WithMany(x => x.UserAddresses)
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.AddressLine).IsRequired().HasColumnType("nvarchar(255)");
            builder.Property(x => x.City).IsRequired().HasColumnType("nvarchar(50)");
            builder.Property(x => x.IsDefault).IsRequired().HasDefaultValue(false);
        }
    }
}
