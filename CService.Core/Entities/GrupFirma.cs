using CService.Core.Entities.General;
using System.ComponentModel.DataAnnotations;
using CService.Core.Constants.Enums;

namespace CService.Core.Entities;

public class GrupFirma : BaseEntity
{
    [Required]
    public string Adi { get; set; } = string.Empty;

    [Required]
    public string FirmaKodu { get; set; } = string.Empty;

    [StringLength(11, MinimumLength = 10, ErrorMessage = "VKN/TCKN 10 veya 11 haneli olmalıdır.")]
    public string? VergiNo { get; set; }
    public string? VergiDairesi { get; set; }

    [EmailAddress(ErrorMessage = "Geçerli bir E-Posta adresi giriniz.")]
    public string? Mail { get; set; }
    public string? Tel1 { get; set; }
    public string? Tel2 { get; set; }
    public string? Not { get; set; }
    public bool IsActive { get; set; } = true;

    public int BolgeId { get; set; }
    public Bolge? Bolge { get; set; }

    public int VatRateId { get; set; }
    public VatRate? VatRate { get; set; }

    public int? WitholdingRateId { get; set; }
    public WitholdingRate? WitholdingRate { get; set; }

    public bool OkulServisi { get; set; }
    public enmOkulOdemeTpi? OkulOdemeTpi { get; set; }

    public int? HavaleBankaHesabiId { get; set; }
    public BankaHesabi? HavaleBankaHesabi { get; set; }

    public int? KrediKartiBankaHesabiId { get; set; }
    public BankaHesabi? KrediKartiBankaHesabi { get; set; }

    public string? Unvan { get; set; }
    public string? Adres { get; set; }
}