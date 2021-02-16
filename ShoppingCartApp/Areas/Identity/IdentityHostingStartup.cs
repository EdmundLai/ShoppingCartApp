using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCartApp.Data;

using Azure.Security.KeyVault.Secrets;
using Azure.Identity;

[assembly: HostingStartup(typeof(ShoppingCartApp.Areas.Identity.IdentityHostingStartup))]
namespace ShoppingCartApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {

            string keyVaultUrl = "https://shoppingcartapp.vault.azure.net/";

            var client = new SecretClient(vaultUri: new Uri(keyVaultUrl), credential: new DefaultAzureCredential());

            string serverName = client.GetSecret("serverName").Value.Value;
            string usersDbName = client.GetSecret("usersDbName").Value.Value;
            string username = client.GetSecret("sql-username").Value.Value;
            string password = client.GetSecret("sql-password").Value.Value;

            string cloudUsersDbConnectionString = $"Server={serverName}.database.windows.net;Database={usersDbName};user id={username};" +
                $"password={password};MultipleActiveResultSets=true";


            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ShoppingCartAppContext>(options =>
                    options.UseSqlServer(
                        cloudUsersDbConnectionString));

                // we comment this out in order to add a IdentityUser and IdentityRole service in the main Startup.cs file
                //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<ShoppingCartAppContext>();
            });
        }
    }
}