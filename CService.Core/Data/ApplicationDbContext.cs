using System.Linq.Expressions;
using CService.Core.Entities;
using CService.Core.Entities.General;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CService.Core.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Bolge> Bolgeler => Set<Bolge>();
    public DbSet<GrupFirma> GrupFirmalar => Set<GrupFirma>();
    public DbSet<BankaHesabi> BankaHesaplari => Set<BankaHesabi>();
    public DbSet<Firma> Firmalar => Set<Firma>();


    #region GENERAL ENTITIES
    public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();
    public DbSet<VatRate> VatRates => Set<VatRate>();
    public DbSet<WitholdingRate> WitholdingRates => Set<WitholdingRate>();
    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Firma>()
            .HasOne(f => f.Bolge)
            .WithMany()
            .HasForeignKey(f => f.BolgeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Firma>()
            .HasOne(f => f.GrupFirma)
            .WithMany()
            .HasForeignKey(f => f.GrupFirmaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Firma>()
            .HasOne(f => f.VatRate)
            .WithMany()
            .HasForeignKey(f => f.VatRateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Firma>()
            .HasOne(f => f.WitholdingRate)
            .WithMany()
            .HasForeignKey(f => f.WitholdingRateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Firma>()
            .HasOne(f => f.HavaleBankaHesabi)
            .WithMany()
            .HasForeignKey(f => f.HavaleBankaHesabiId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Firma>()
            .HasOne(f => f.KrediKartiBankaHesabi)
            .WithMany()
            .HasForeignKey(f => f.KrediKartiBankaHesabiId)
            .OnDelete(DeleteBehavior.Restrict);

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                continue;

            var parameter = Expression.Parameter(entityType.ClrType, "e");
            var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
            var condition = Expression.Equal(property, Expression.Constant(false));
            var lambda = Expression.Lambda(condition, parameter);

            builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
        }
    }
}