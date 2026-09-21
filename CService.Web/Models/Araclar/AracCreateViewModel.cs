using System.ComponentModel.DataAnnotations;
using CService.Core.Constants.Enums;
using CService.Core.Entities;

namespace CService.Web.Models.Araclar;

public class AracCreateViewModel
{
    [Required]
    public int FirmaId { get; set; }

    public int? ExistingAracSahibiId { get; set; }

    public string? YeniSahibiKod { get; set; }
    public string? YeniSahibiAdi { get; set; }
    public string? YeniSahibiSoyad { get; set; }
    public string? YeniSahibiTcKimlikNo { get; set; }
    public string? YeniSahibiTel { get; set; }

    [Required]
    public string Plaka { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    public enmOdemeDurumu OdemeDurumu { get; set; }

    [Required]
    public int AracCinsiId { get; set; }

    [Required]
    public int AracMarkaId { get; set; }

    [Required]
    public int AracTipiId { get; set; }

    public string? Modeli { get; set; }
    public int? Kapasite { get; set; }
    public string? RuhsatNo { get; set; }
    public string? MotorNo { get; set; }
    public string? SasiNo { get; set; }
    public bool Klima { get; set; }
    public DateOnly? IlkGirisTarihi { get; set; }

    public string? RuhsatSahibi { get; set; }
    public string? RuhsatSahibiKimlikNo { get; set; }
    public string? SoforAdi { get; set; }
    public string? SoforKimlikNo { get; set; }
    public string? SoforTelefon { get; set; }
    public string? HostesAdi { get; set; }
    public string? HostesKimlikNo { get; set; }
    public string? HostesTelefon { get; set; }
}