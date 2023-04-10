﻿using IdentityPractice.Configurations;
using IdentityPractice.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityPractice.Models.Context
{
    public class ContextDeneme:IdentityDbContext<AppUser,AppRole,int>
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }

        public ContextDeneme(DbContextOptions<ContextDeneme> option) : base(option)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PostConfigurations()).ApplyConfiguration(new CommentConfigurations()).ApplyConfiguration(new CategoryConfigurations());


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
