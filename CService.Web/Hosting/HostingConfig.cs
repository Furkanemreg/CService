using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CService.Web.Hosting;

public static class HostingConfig
{
    // Hem Web tek başına (Program.cs) hem de Desktop içinden (App.xaml.cs) çağırır.
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services
            .AddControllersWithViews()
            // Desktop'tan host edildiğinde controller + derlenmiş view'ların bulunması garanti olsun:
            .AddApplicationPart(typeof(HostingConfig).Assembly);

        // TODO (EF ileride): SQL Server / LocalDB
        // builder.Services.AddDbContext<AppDbContext>(o =>
        //     o.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
    }

    public static void ConfigurePipeline(WebApplication app)
    {
        app.UseStaticFiles();
        app.UseRouting();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }
}