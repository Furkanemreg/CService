using System.ComponentModel.DataAnnotations;

namespace CService.Core.Entities;

public class Hostes : BaseEntity
{
    [Required]
    public string Kod { get; set; } = string.Empty;

    [Required]
    public string AdSoyad { get; set; } = string.Empty;

    [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır.")]
    public string? TcKimlikNo { get; set; }

    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    public string? Email { get; set; }

    public string? Tel { get; set; }
}