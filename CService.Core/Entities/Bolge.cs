using System.ComponentModel.DataAnnotations;

namespace CService.Core.Entities;

public class Bolge : BaseEntity
{
    [Required]
    public string Ad { get; set; } = string.Empty;

    [Required]
    public string Kod { get; set; } = string.Empty;
}