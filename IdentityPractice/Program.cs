using IdentityPractice.Entities;
using IdentityPractice.Models.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityPractice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ContextDeneme>(opt =>
            {



                opt.UseSqlServer(builder.Configuration.GetConnectionString("YusaDbConnection"));

                //opt.UseSqlServer(builder.Configuration.GetConnectionString("DenizDbConnection"));

                //opt.UseSqlServer(builder.Configuration.GetConnectionString("CadoDbConnection"));

            });


            builder.Services.AddIdentity<AppUser,AppRole>(opt=>
            { 
                opt.Password.RequireDigit = false;
                //opt.Password.RequiredLength = 1;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.SignIn.RequireConfirmedEmail = false;

            }).AddEntityFrameworkStores<ContextDeneme>();

            builder.Services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                opt.Cookie.Name = "BlogCookie";
                opt.ExpireTimeSpan = TimeSpan.FromDays(25);
                opt.LoginPath = new PathString("/Login/SignIn");

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=SignIn}/{id?}");

            app.Run();
        }
    }
}