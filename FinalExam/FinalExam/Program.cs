using FinalExam.DAL;
using FinalExam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalExam
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireDigit = true;


                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._@";
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.MapControllerRoute
                (
                    name: "areas",
                    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                );


            app.MapControllerRoute
                (
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );


            app.UseStaticFiles();

            app.Run();
        }
    }
}
