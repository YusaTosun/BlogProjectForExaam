using IdentityPractice.Configurations;
using IdentityPractice.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityPractice.Models.Context
{
    public class ContextDeneme:IdentityDbContext<AppUser,AppRole,int>
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public ContextDeneme(DbContextOptions<ContextDeneme> option) : base(option)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>().HasKey(x => x.StudentId);
            builder.Entity<Student>().HasOne(x => x.Teacher).WithMany(x=>x.Students).HasForeignKey(x=>x.TeacherId);

            builder.Entity<Teacher>().HasKey(x => x.TeacherId);

           base.OnModelCreating(builder);

            #region AppRole
            builder.Entity<AppRole>().HasData(new AppRole { Id = 1, Name = "Admin",NormalizedName="ADMIN" }, new AppRole { Id = 2, Name = "Member",NormalizedName="MEMBER" });
            #endregion
            #region AppUser
            builder.Entity<AppUser>().HasData(new AppUser { Id=1,Email="yusatosun.yt@gmail.com",Gender="Erkek",UserName="yusa"});
            #endregion
            #region UserRole
            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>() { RoleId=1,UserId=1});
            #endregion

        }
    }
}
