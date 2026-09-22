using System.ComponentModel.DataAnnotations;

namespace CService.Web.Models.Ayarlar;

public class OpeningPasswordViewModel
{
    [Required]
    public string EskiSifre { get; set; } = string.Empty;

    [Required]
    public string YeniSifre { get; set; } = string.Empty;

    [Required]
    [Compare(nameof(YeniSifre), ErrorMessage = "Yeni şifreler eşleşmiyor.")]
    public string YeniSifreTekrar { get; set; } = string.Empty;
}