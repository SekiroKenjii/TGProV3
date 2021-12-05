using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class UserLoginTokenConfiguration : IEntityTypeConfiguration<UserLoginToken>
    {
        public void Configure(EntityTypeBuilder<UserLoginToken> builder)
        {
            builder.ToTable("UserLoginTokens");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.User).WithMany(x => x.UserLoginTokens)
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Token).IsRequired();
        }
    }
}
