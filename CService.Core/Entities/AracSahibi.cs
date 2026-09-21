using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CService.Core.Constants.Enums;

namespace CService.Core.Entities;

public class AracSahibi : BaseEntity
{
    [Required]
    public string Kod { get; set; } = string.Empty;

    [Required]
    public string Adi { get; set; } = string.Empty;

    [Required]
    public string Soyad { get; set; } = string.Empty;

    [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır.")]
    public string? TcKimlikNo { get; set; }

    public string? Tel { get; set; }
    public string? Adres { get; set; }
    public string? VergiDairesi { get; set; }
    public string? VergiNo { get; set; }
    public string? Vekil { get; set; }
    public string? VekilTel { get; set; }
    public string? Not { get; set; }
    public string? Sorumlu { get; set; }
    public bool IsActive { get; set; } = true;

    public int? OdemeGrubuId { get; set; }
    public OdemeGrubu? OdemeGrubu { get; set; }

    public enmOdemeTipi OdemeTipi { get; set; }
    public enmVergiUsulu VergiUsulu { get; set; }

    public int? BankaHesabiId { get; set; }
    public BankaHesabi? BankaHesabi { get; set; }

    public string? HesapSahibi { get; set; }
    public string? HesapNo { get; set; }
    public string? Iban { get; set; }

    public decimal? YakitOrani { get; set; }
    public decimal? OkulKomisyonu { get; set; }

    [NotMapped]
    public string TamAdi => $"{Adi} {Soyad}".Trim();
}