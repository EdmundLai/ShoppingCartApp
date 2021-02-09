using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCartApp.Data;

[assembly: HostingStartup(typeof(ShoppingCartApp.Areas.Identity.IdentityHostingStartup))]
namespace ShoppingCartApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ShoppingCartAppContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ShoppingCartAppContextConnection")));

                // we comment this out in order to add a IdentityUser and IdentityRole service in the main Startup.cs file
                //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<ShoppingCartAppContext>();
            });
        }
    }
}