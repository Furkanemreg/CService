using CService.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CService.Web.Hosting;

public static class HostingConfig
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services
            .AddControllersWithViews()
            .AddApplicationPart(typeof(HostingConfig).Assembly);

        builder.Services.AddCoreServices(builder.Configuration.GetConnectionString("Default")!);
    }

    public static void ConfigurePipeline(WebApplication app)
    {
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }
}