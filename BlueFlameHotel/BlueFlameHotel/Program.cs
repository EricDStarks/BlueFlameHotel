using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Swashbuckle.AspNetCore;
using BlueFlameHotel.Data;
using BlueFlameHotel.Models.Interfaces;
using BlueFlameHotel.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;

namespace BlueFlameHotel
{
    public class Program
    {
       
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //var host = CreateHostBuilder(args).Build();

            //UpdateDatabase();

            // Add services to the container
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<BlueFlameHotelContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<BlueFlameHotelContext>();
            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false);
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            //Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //Default Hsts value is 30 days. You may want to change this for production scenarios. See https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles; });

            builder.Services.AddSwaggerGen(options =>
            {
                // Make sure get the "using Statement"
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Blue Flame Hotel",
                    Version = "v1",
                });
            });

            /*builder.Services.addContext*/
            builder.Services.AddDbContext<BlueFlameHotelContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddTransient<IHotel, HotelService>();
            app.MapGet("/", () => "Welcome to the Blue Flame Hotel!");
            var newApp = builder.Build();

            app.UseSwagger(options => {
                options.RouteTemplate = "/api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/api/v1/swagger.json", "Student Demo");
                options.RoutePrefix = "docs";
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            //Insert URL here

           

            app.Run();
        }

        private static void UpdateDatabase (IServiceProvider services)
        {
            using (var serviceScope =  services.CreateScope()) 
            {
                using (var db = serviceScope.ServiceProvider.GetService<BlueFlameHotelContext>())
                {
                    db.Database.Migrate();
                }
            }
        }
    }
}