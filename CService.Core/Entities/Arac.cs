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

    public int AracCinsiId { get; set; }
    public AracCinsi? AracCinsi { get; set; }

    public int AracMarkaId { get; set; }
    public AracMarka? AracMarka { get; set; }

    public int AracTipiId { get; set; }
    public AracTipi? AracTipi { get; set; }

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

    public int? HostesId { get; set; }
    public Hostes? Hostes { get; set; }

    public int FirmaId { get; set; }
    public Firma? Firma { get; set; }

    public int AracSahibiId { get; set; }
    public AracSahibi? AracSahibi { get; set; }
}