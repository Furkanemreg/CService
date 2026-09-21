using CService.Core.Data.Seeders;
using CService.Core.Middlewares;
using CService.Web.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
HostingConfig.ConfigureServices(builder);

var app = builder.Build();
HostingConfig.ConfigurePipeline(app);

using (var scope = app.Services.CreateScope())
{
    await IdentitySeeder.SeedAsync(scope.ServiceProvider);
}

app.Run();