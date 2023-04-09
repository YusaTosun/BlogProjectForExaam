using IdentityPractice.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityPractice.Configurations
{
    public class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x=>x.StudentId);

            builder.HasOne(x=>x.Teacher).WithMany(x=>x.Students).HasForeignKey(x=>x.TeacherId);
        }
    }
}
