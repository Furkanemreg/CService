using System.ComponentModel.DataAnnotations;

namespace CService.Core.Entities;

public class Yetkili : BaseEntity
{
    [Required]
    public string Kod { get; set; } = string.Empty;

    [Required]
    public string AdSoyad { get; set; } = string.Empty;
}