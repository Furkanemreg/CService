using System.ComponentModel.DataAnnotations;

namespace CService.Core.Entities;

public class AracTipi : BaseEntity
{
    [Required]
    public string Ad { get; set; } = string.Empty;
    public string? Kod { get; set; }
}