using IdentityPractice.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityPractice.Configurations
{
    public class CommentConfigurations : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x=>x.CommentId);
           builder.HasOne(x=>x.User).WithMany(x=>x.Comments).HasForeignKey(x=>x.UserId).OnDelete(DeleteBehavior.NoAction);  /// todo:bu  davranışı daha sonra gerekirse değiştir
           builder.HasOne(x=>x.Post).WithMany(x=>x.Comments).HasForeignKey(x=>x.PostId); 
        }
    }
}
