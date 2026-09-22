using System.ComponentModel.DataAnnotations;
using CService.Core.Constants.Enums;

namespace CService.Core.Entities;

public class OdemeTipi : BaseEntity
{
    [Required]
    public string Kod { get; set; } = string.Empty;

    [Required]
    public string Ad { get; set; } = string.Empty;

    public enmKdvDurumu Kdv { get; set; }
}