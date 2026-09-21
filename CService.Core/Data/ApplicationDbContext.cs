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
    public DbSet<AracSahibi> AracSahipleri => Set<AracSahibi>();
    public DbSet<Arac> Araclar => Set<Arac>();
    public DbSet<OdemeGrubu> OdemeGruplari => Set<OdemeGrubu>();
    public DbSet<AracCinsi> AracCinsleri => Set<AracCinsi>();
    public DbSet<AracMarka> AracMarkalari => Set<AracMarka>();
    public DbSet<AracTipi> AracTipleri => Set<AracTipi>();
    #region GENERAL ENTITIES
    public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();
    public DbSet<VatRate> VatRates => Set<VatRate>();
    public DbSet<WitholdingRate> WitholdingRates => Set<WitholdingRate>();
    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Firma
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
        #endregion

        #region GrupFirma

        builder.Entity<GrupFirma>()
            .HasOne(g => g.Bolge)
            .WithMany()
            .HasForeignKey(g => g.BolgeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<GrupFirma>()
            .HasOne(g => g.VatRate)
            .WithMany()
            .HasForeignKey(g => g.VatRateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<GrupFirma>()
            .HasOne(g => g.WitholdingRate)
            .WithMany()
            .HasForeignKey(g => g.WitholdingRateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<GrupFirma>()
            .HasOne(g => g.HavaleBankaHesabi)
            .WithMany()
            .HasForeignKey(g => g.HavaleBankaHesabiId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<GrupFirma>()
            .HasOne(g => g.KrediKartiBankaHesabi)
            .WithMany()
            .HasForeignKey(g => g.KrediKartiBankaHesabiId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region ARAÇ / ARAÇ SAHİBİ
        builder.Entity<AracSahibi>()
            .HasOne(a => a.BankaHesabi)
            .WithMany()
            .HasForeignKey(a => a.BankaHesabiId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AracSahibi>()
            .HasOne(a => a.OdemeGrubu)
            .WithMany()
            .HasForeignKey(a => a.OdemeGrubuId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Arac>()
            .HasOne(a => a.Firma)
            .WithMany()
            .HasForeignKey(a => a.FirmaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Arac>()
            .HasOne(a => a.AracSahibi)
            .WithMany()
            .HasForeignKey(a => a.AracSahibiId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Arac>().HasOne(a => a.AracCinsi).WithMany().HasForeignKey(a => a.AracCinsiId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Arac>().HasOne(a => a.AracMarka).WithMany().HasForeignKey(a => a.AracMarkaId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Arac>().HasOne(a => a.AracTipi).WithMany().HasForeignKey(a => a.AracTipiId).OnDelete(DeleteBehavior.Restrict);
        #endregion

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