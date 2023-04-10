using IdentityPractice.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityPractice.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x=>x.CategoryId);
            builder.HasMany(x=>x.Posts).WithOne(x=>x.Category).HasForeignKey(x=>x.PostId);
        }
    }
}
