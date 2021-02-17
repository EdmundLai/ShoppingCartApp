using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ShoppingCartApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Services.AppAuthentication;
using Azure.Identity;
using Microsoft.Azure.KeyVault;

namespace ShoppingCartApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //string localShoppingAppContextString = $"Server=(localdb)\\mssqllocaldb;Database=ShoppingAppContext-1;Trusted_Connection=True;MultipleActiveResultSets=true";

            var azureServiceTokenProvider = new AzureServiceTokenProvider();

            string keyVaultUrl = "https://shoppingcartapp.vault.azure.net/secrets";

            //var sc = new SecretClient(keyVaultUrl, new DefaultAzureCredential())

            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

            string serverName = kv.GetSecretAsync($"{keyVaultUrl}/serverName").Result.Value;
            string mainDbName = kv.GetSecretAsync($"{keyVaultUrl}/mainDbName").Result.Value;
            string username = kv.GetSecretAsync($"{keyVaultUrl}/sql-username").Result.Value;
            string password = kv.GetSecretAsync($"{keyVaultUrl}/sql-password").Result.Value;

            string cloudShoppingAppContextString = $"Server={serverName}.database.windows.net;Database={mainDbName};user id={username};" +
                $"password={password};MultipleActiveResultSets=true";

            //services.AddDbContext<ShoppingAppContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("ShoppingAppContext")));

            services.AddDbContext<ShoppingAppContext>(options =>
                options.UseSqlServer(cloudShoppingAppContextString));

            // added identity service to be used by Role controller
            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                 .AddDefaultUI()
                 .AddEntityFrameworkStores<ShoppingCartAppContext>()
                 .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // added to enable Microsoft Identity Authentication flow
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // added to support scaffolded Identity item
                endpoints.MapRazorPages();
            });
        }
    }
}
