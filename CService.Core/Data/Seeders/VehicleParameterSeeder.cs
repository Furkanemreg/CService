using CService.Core.Entities;
using CService.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CService.Core.Data.Seeders;

public static class VehicleParameterSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var unitOfWork = services.GetRequiredService<IUnitOfWork>();

        var cinsiRepo = unitOfWork.Repository<AracCinsi>();
        if (!(await cinsiRepo.GetAllAsync()).Any())
        {
            await cinsiRepo.AddAsync(new AracCinsi { Ad = "MİNİBÜS" });
            await cinsiRepo.AddAsync(new AracCinsi { Ad = "OTOBÜS" });
        }

        var markaRepo = unitOfWork.Repository<AracMarka>();
        if (!(await markaRepo.GetAllAsync()).Any())
        {
            await markaRepo.AddAsync(new AracMarka { Ad = "CITROEN" });
            await markaRepo.AddAsync(new AracMarka { Ad = "FIAT" });
            await markaRepo.AddAsync(new AracMarka { Ad = "MERCEDES" });
        }

        var tipiRepo = unitOfWork.Repository<AracTipi>();
        if (!(await tipiRepo.GetAllAsync()).Any())
        {
            await tipiRepo.AddAsync(new AracTipi { Ad = "DUCATO" });
            await tipiRepo.AddAsync(new AracTipi { Ad = "JUMPER" });
            await tipiRepo.AddAsync(new AracTipi { Ad = "SPRINTER" });
        }

        await unitOfWork.SaveChangesAsync();
    }
}
