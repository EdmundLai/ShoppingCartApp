using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCartApp.Data;

//using Microsoft.Azure.KeyVault.Secrets;

using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault;

[assembly: HostingStartup(typeof(ShoppingCartApp.Areas.Identity.IdentityHostingStartup))]
namespace ShoppingCartApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {

            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

            string keyVaultUrl = "https://shoppingcartapp.vault.azure.net/secrets";

            string serverName = kv.GetSecretAsync($"{keyVaultUrl}/serverName").Result.Value;
            string usersDbName = kv.GetSecretAsync($"{keyVaultUrl}/usersDbName").Result.Value;
            string username = kv.GetSecretAsync($"{keyVaultUrl}/sql-username").Result.Value;
            string password = kv.GetSecretAsync($"{keyVaultUrl}/sql-password").Result.Value;


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