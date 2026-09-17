using Microsoft.AspNetCore.Identity;

namespace CService.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public bool IsDeleted { get; set; } = false;
}
