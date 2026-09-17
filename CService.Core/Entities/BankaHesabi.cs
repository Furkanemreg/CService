using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CService.Core.Entities;

public class BankaHesabi : BaseEntity
{
    public string BankaAdi { get; set; } = string.Empty;
    public string? SubeAdi { get; set; }
    public string? HesapNo { get; set; }

    [MaxLength(26)]
    public string? Iban { get; set; }
    public string? Aciklama { get; set; }
}