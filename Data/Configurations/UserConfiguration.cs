using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").HasDefaultValueSql("NEWID()");

            builder.HasIndex(x => x.PhoneNumber).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.FullName).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.PhoneNumber).IsRequired().HasColumnType("varchar(16)");
            builder.Property(x => x.Email).IsRequired().HasColumnType("varchar(255)");
            builder.Property(x => x.Avatar).IsRequired().HasColumnType("varchar(255)");
            builder.Property(x => x.AvatarId).IsRequired().HasColumnType("varchar(250)");
            builder.Property(x => x.PasswordHash).IsRequired().HasColumnType("varchar(max)");
            builder.Property(x => x.PasswordSalt).IsRequired().HasColumnType("varchar(max)");
            builder.Property(x => x.Gender).IsRequired().HasColumnType("TINYINT");
            builder.Property(x => x.IsBlocked).IsRequired().HasDefaultValue(false);
        }
    }
}
