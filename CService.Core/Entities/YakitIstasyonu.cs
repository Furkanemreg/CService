using System.ComponentModel.DataAnnotations;

namespace CService.Core.Entities;

public class YakitIstasyonu : BaseEntity
{
    [Required]
    public string Kod { get; set; } = string.Empty;

    [Required]
    public string Ad { get; set; } = string.Empty;

    public decimal Komisyon { get; set; }
}