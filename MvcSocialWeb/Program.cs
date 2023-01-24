using Microsoft.EntityFrameworkCore;
using MvcSocialWeb.Data;

namespace MvcSocialWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            //Services

            var builder = WebApplication.CreateBuilder(args);

            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<SocialWebContext>(option => option.UseSqlServer(connection), ServiceLifetime.Singleton);

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }
    }
}