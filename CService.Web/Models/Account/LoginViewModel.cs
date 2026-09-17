using System.ComponentModel.DataAnnotations;

namespace CService.Web.Models.Account;

public class LoginViewModel
{
    [Required(ErrorMessage = "E-Posta zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir E-Posta adresi girin.")]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre zorunludur.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Beni hatırla")]
    public bool RememberMe { get; set; }
}