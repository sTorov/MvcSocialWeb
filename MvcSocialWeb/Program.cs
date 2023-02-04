using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcSocialWeb.Data;
using MvcSocialWeb.Data.DBModel.Friend;
using MvcSocialWeb.Data.DBModel.Messages;
using MvcSocialWeb.Data.DBModel.Users;
using MvcSocialWeb.Data.Repositories;
using MvcSocialWeb.Middlewares.Extensions;
using System.Reflection;

namespace MvcSocialWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            //Services

            var builder = WebApplication.CreateBuilder(args);

            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<SocialWebContext>(option => option.UseSqlServer(connection, b => b.MigrationsAssembly("MvcSocialWeb")))
                .AddUnitOfWork()
                .AddCustomRepository<Friend, FriendRepository>()
                .AddCustomRepository<Message, MessageRepository>()
                .AddControllerServices()
                .AddUserGeneration();

            var assembly = Assembly.GetAssembly(typeof(MapperProfile));
            builder.Services.AddAutoMapper(assembly);

            builder.Services.AddIdentity<User, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 5;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<SocialWebContext>();

            builder.Services.AddAuthorization(options =>                          
            {
                options.AddPolicy("admin", policy =>
                {
                    policy.RequireUserName("admin");
                });
            });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            //Configs

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }


    }
}