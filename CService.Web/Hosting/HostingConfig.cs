using CService.Core.Extensions;
using CService.Core.Interfaces;
using CService.Web.Services;
using Microsoft.AspNetCore.Authorization;
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

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

        builder.Services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });
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