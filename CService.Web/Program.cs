using CService.Web.Hosting;

var builder = WebApplication.CreateBuilder(args);
HostingConfig.ConfigureServices(builder);

var app = builder.Build();
HostingConfig.ConfigurePipeline(app);

app.Run();