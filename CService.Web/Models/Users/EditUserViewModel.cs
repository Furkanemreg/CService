using System.ComponentModel.DataAnnotations;

namespace CService.Web.Models.Users;

public class EditUserViewModel
{
    public string Id { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Rol seçiniz.")]
    [Display(Name = "Rol")]
    public string Role { get; set; } = "User";
}