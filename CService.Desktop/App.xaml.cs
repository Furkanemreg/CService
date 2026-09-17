using System;
using System.Windows;
using CService.Core.Data;
using CService.Core.Data.Seeders;
using CService.Web;
using CService.Web.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CService.Desktop;

public partial class App : Application
{
    private WebApplication? _webApp;
    public const string BaseUrl = "http://127.0.0.1:5005";

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var options = new WebApplicationOptions
        {
            ContentRootPath = AppContext.BaseDirectory,
            Args = e.Args
        };

        var builder = WebApplication.CreateBuilder(options);
        builder.Configuration.AddJsonFile("desktopsettings.json", optional: true, reloadOnChange: true);
        builder.WebHost.UseUrls(BaseUrl);

        HostingConfig.ConfigureServices(builder);

        _webApp = builder.Build();
        HostingConfig.ConfigurePipeline(_webApp);

        using (var scope = _webApp.Services.CreateScope())
        {
            await IdentitySeeder.SeedAsync(scope.ServiceProvider);
        }

        await _webApp.StartAsync();

        new MainWindow(BaseUrl).Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        if (_webApp is not null)
            await _webApp.StopAsync();
        base.OnExit(e);
    }
}