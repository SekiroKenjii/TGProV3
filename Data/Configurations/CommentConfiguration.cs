using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.User).WithMany(x => x.Comments)
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Product).WithMany(x => x.Comments)
                .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.ParentComment).WithMany(x => x.ChildComments)
                .HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.CommentContent).IsRequired().HasColumnType("nvarchar(500)");
            builder.Property(x => x.CommentedAt).IsRequired().HasDefaultValue(DateTime.UtcNow);
        }
    }
}
