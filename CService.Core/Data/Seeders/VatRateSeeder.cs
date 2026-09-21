using CService.Core.Entities.General;
using CService.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CService.Core.Data.Seeders;

public static class VatRateSeeder
{
    private static readonly decimal[] Rates = { 0m, 1m, 10m, 18m, 20m };

    public static async Task SeedAsync(IServiceProvider services)
    {
        var unitOfWork = services.GetRequiredService<IUnitOfWork>();
        var repository = unitOfWork.Repository<VatRate>();

        var existing = await repository.GetAllAsync();

        foreach (var rate in Rates)
        {
            if (existing.Any(x => x.Rate == rate))
                continue;

            await repository.AddAsync(new VatRate { Rate = rate });
        }

        await unitOfWork.SaveChangesAsync();
    }
}