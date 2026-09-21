using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CService.Core.Constants.Enums;

namespace CService.Core.Entities;

public class Arac : BaseEntity
{
    [Required]
    public string Plaka { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    public enmOdemeDurumu OdemeDurumu { get; set; }

    public enmAracCinsi Cinsi { get; set; }
    public enmAracMarka Marka { get; set; }
    public enmAracTipi Tipi { get; set; }

    public string? Modeli { get; set; }
    public int? Kapasite { get; set; }
    public string? RuhsatNo { get; set; }
    public string? MotorNo { get; set; }
    public string? SasiNo { get; set; }
    public bool Klima { get; set; }
    public DateTime? IlkGirisTarihi { get; set; }

    public string? RuhsatSahibi { get; set; }
    public string? RuhsatSahibiKimlikNo { get; set; }

    public string? SoforAdi { get; set; }
    public string? SoforKimlikNo { get; set; }
    public string? SoforTelefon { get; set; }

    public string? HostesAdi { get; set; }
    public string? HostesKimlikNo { get; set; }
    public string? HostesTelefon { get; set; }

    public int FirmaId { get; set; }
    public Firma? Firma { get; set; }

    public int AracSahibiId { get; set; }
    public AracSahibi? AracSahibi { get; set; }
}