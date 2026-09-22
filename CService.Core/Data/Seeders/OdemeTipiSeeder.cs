using CService.Core.Constants.Enums;
using CService.Core.Entities;
using CService.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CService.Core.Data.Seeders;

public static class OdemeTipiSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var unitOfWork = services.GetRequiredService<IUnitOfWork>();
        var repo = unitOfWork.Repository<OdemeTipi>();

        if ((await repo.GetAllAsync()).Any()) return;

        await repo.AddAsync(new OdemeTipi { Kod = "ELDEN", Ad = "Elden Ödeme", Kdv = enmKdvDurumu.Etkilemez });
        await repo.AddAsync(new OdemeTipi { Kod = "HAVALE", Ad = "Banka Havalesi", Kdv = enmKdvDurumu.Etkilemez });
        await repo.AddAsync(new OdemeTipi { Kod = "ODEME_YAPMA", Ad = "Ödeme Yapma", Kdv = enmKdvDurumu.Etkilemez });

        await unitOfWork.SaveChangesAsync();
    }
}