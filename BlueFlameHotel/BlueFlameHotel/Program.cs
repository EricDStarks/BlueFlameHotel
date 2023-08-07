using BlueFlameHotel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlueFlameHotel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllersWithViews().AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles; });

            /*builder.Services.addContext*/
            builder.Services.AddDbContext<BlueFlameHotelContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //app.MapGet("/", () => "Welcome to the Blue Flame Hotel!");
            var app = builder.Build();
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{lid?}");

            //Insert URL here

           

            app.Run();
        }
    }
}