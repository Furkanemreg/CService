using System.ComponentModel.DataAnnotations;

namespace CService.Core.Entities;

public class Guzergah : BaseEntity
{
    [Required]
    public int FirmaId { get; set; }
    public Firma? Firma { get; set; }

    [Required]
    public string Kod { get; set; } = string.Empty;

    public string? Kod2 { get; set; }

    [Required]
    public string Ad { get; set; } = string.Empty;

    public int? BolgeId { get; set; }
    public Bolge? Bolge { get; set; }

    public int? YetkiliId { get; set; }
    public Yetkili? Yetkili { get; set; }

    public int Ay { get; set; }
    public int Yil { get; set; }

    public string? Aciklama { get; set; }

    public int? HostesId { get; set; }
    public Hostes? Hostes { get; set; }

    public int? Kapasite { get; set; }
    public decimal? KmTekYon { get; set; }
    public int? SeferSayisi { get; set; }
    public string? ServisIstikameti { get; set; }
}