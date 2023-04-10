using IdentityPractice.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityPractice.Configurations
{
    public class PostConfigurations : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x=>x.PostId);
            
            builder.HasOne(x=>x.Author).WithMany(x=>x.Posts).HasForeignKey(x=>x.AuthorId);
        }
    }
}
