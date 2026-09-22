using CService.Core.Entities;
using CService.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CService.Core.Data.Seeders;

public static class GlobalSettingSeeder
{
    public static async Task SeedAsync(IServiceProvider services, IConfiguration configuration)
    {
        var unitOfWork = services.GetRequiredService<IUnitOfWork>();
        var repo = unitOfWork.Repository<GlobalSetting>();

        if ((await repo.GetAllAsync()).Any()) return;

        var defaultPassword = configuration["AppSettings:DefaultOpeningPassword"] ?? "1234";
        await repo.AddAsync(new GlobalSetting { OpeningPassword = defaultPassword });
        await unitOfWork.SaveChangesAsync();
    }
}
