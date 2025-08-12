using BuisnessLogic.DataContext;
using BuisnessLogic.Repositories;
using CrfDesign.Server.WebAPI.Data;
using CrfDesign.Server.WebAPI.Models;
using CrfDesign.Server.WebAPI.Models.AdminManagement;
using CrfDesign.Server.WebAPI.Models.LoginModels;
using CrfDesign.Server.WebAPI.Models.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CrfDesign.Server.WebAPI
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

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpContextAccessor(); // needed once globally

            services.AddDbContext<CrfDesignContext>((serviceProvider, optionsBuilder) =>
            {
                var connectionString = Configuration.GetConnectionString("CrfDesignConnection");
                optionsBuilder.UseSqlServer(connectionString);
            });

            services.AddSingleton<IRuntimeEnvironment, RuntimeEnvironment>();
            services.AddSingleton<IInMemoryCrfDataStore, InMemoryCrfDataStore>();

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddRazorPages();
            services.AddControllersWithViews();

            services.AddIdentity<Investigator, IdentityRole>(options =>
                                options.SignIn.RequireConfirmedAccount = true)
                                    .AddRoles<IdentityRole>()
                                    .AddEntityFrameworkStores<ApplicationDbContext>()
                                    .AddDefaultTokenProviders()
                                    .AddDefaultUI();

            services.AddScoped<CrfOptionCategoryManager>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllerRoute(
                                name: "default",
                                pattern: "{controller=Home}/{action=Index}/{id?}");
                            endpoints.MapRazorPages();
                        });


            // Create roles if missing
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            IdentityDataInitializer.SeedRolesAsync(roleManager).Wait();
        }

    }
}
