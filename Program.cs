using Microsoft.EntityFrameworkCore;
using MVCPronia.DAL;
using MVCPronia.Services.Implementations;
using MVCPronia.Services.Interfaces;
using System;

namespace MVCPronia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddScoped<ILayoutService, LayoutService>();

            var app = builder.Build();
            app.UseStaticFiles();

            app.MapControllerRoute(
                "admin",
                "{area:exists}/{controller=home}/{action=index}/{id?}"
                );

            app.MapControllerRoute(
                "default",
                "{controller=home}/{action=index}/{id?}"
                );

            app.Run();
        }
    }
}
