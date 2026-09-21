using System.Security.Claims;
using CService.Core.Data;
using CService.Core.Data.Seeders;
using CService.Core.Entities;
using CService.Core.Interfaces;
using CService.Web.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CService.Web.Controllers;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IActivityLogger _activityLogger;

    public UsersController(UserManager<ApplicationUser> userManager, IActivityLogger activityLogger)
    {
        _userManager = userManager;
        _activityLogger = activityLogger;
    }

    private string? CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

    public async Task<IActionResult> Index()
    {
        var items = new List<UserListItemViewModel>();

        foreach (var user in _userManager.Users.Where(u => !u.IsDeleted).ToList())
        {
            items.Add(new UserListItemViewModel
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                Roles = await _userManager.GetRolesAsync(user),
                IsLocked = await _userManager.IsLockedOutAsync(user)
            });
        }

        return View(items);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Roles = new SelectList(
            new[]
            {
                new { Value = "Admin", Text = "Yönetici" },
                new { Value = "Manager", Text = "Müdür" },
                new { Value = "User", Text = "Kullanıcı" }
            },
            "Value",
            "Text"
        );

        return View(new CreateUserViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Roles = new SelectList(
                new[]
                {
                    new { Value = "Admin", Text = "Yönetici" },
                    new { Value = "Manager", Text = "Müdür" },
                    new { Value = "User", Text = "Kullanıcı" }
                },
                "Value",
                "Text"
            );
            return View(model);
        }

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = true,
            LockoutEnabled = true
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            
            ViewBag.Roles = new SelectList(
                new[]
                {
                    new { Value = "Admin", Text = "Yönetici" },
                    new { Value = "Manager", Text = "Müdür" },
                    new { Value = "User", Text = "Kullanıcı" }
                },
                "Value",
                "Text"
            );
            return View(model);
        }

        await _userManager.AddToRoleAsync(user, model.Role);

        await _activityLogger.LogAsync("Kullanıcı Oluşturuldu", user.Id, user.Email, $"Rol: {model.Role}");

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null) return NotFound();

        var roles = await _userManager.GetRolesAsync(user);

        ViewBag.Roles = new SelectList(
            new[]
            {
                    new { Value = "Admin", Text = "Yönetici" },
                    new { Value = "Manager", Text = "Müdür" },
                    new { Value = "User", Text = "Kullanıcı" }
            },
            "Value",
            "Text"
        );

        return View(new EditUserViewModel
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            Role = roles.FirstOrDefault() ?? "User"
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditUserViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id);
        if (user is null) return NotFound();

        if (!ModelState.IsValid)
        {
            ViewBag.Roles = new SelectList(
                new[]
                {
                    new { Value = "Admin", Text = "Yönetici" },
                    new { Value = "Manager", Text = "Müdür" },
                    new { Value = "User", Text = "Kullanıcı" }
                },
                "Value",
                "Text"
            );
            return View(model);
        }

        user.Email = model.Email;
        user.UserName = model.Email;
        await _userManager.UpdateAsync(user);

        var currentRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentRoles);
        await _userManager.AddToRoleAsync(user, model.Role);

        await _activityLogger.LogAsync("Kullanıcı Güncellendi", user.Id, user.Email, $"Yeni Rol: {model.Role}");

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleLock(string id)
    {
        if (id == CurrentUserId)
        {
            TempData["Error"] = "Kendi hesabınızı kısıtlayamazsınız.";
            return RedirectToAction(nameof(Index));
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user is null) return NotFound();

        var isLocked = await _userManager.IsLockedOutAsync(user);
        await _userManager.SetLockoutEndDateAsync(user, isLocked ? null : DateTimeOffset.MaxValue);

        await _activityLogger.LogAsync(isLocked ? "Kısıtlama Kaldırıldı" : "Kullanıcı Kısıtlandı", user.Id, user.Email);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        if (id == CurrentUserId)
        {
            TempData["Error"] = "Kendi hesabınızı silemezsiniz.";
            return RedirectToAction(nameof(Index));
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user is null) return NotFound();

        user.IsDeleted = true;
        await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
        await _userManager.UpdateAsync(user);

        await _activityLogger.LogAsync("Kullanıcı Silindi", user.Id, user.Email);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> ChangePassword(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null) return NotFound();

        return View(new ChangePasswordViewModel { UserId = user.Id, Email = user.Email ?? string.Empty });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user is null) return NotFound();

        if (!ModelState.IsValid)
            return View(model);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(model);
        }

        await _activityLogger.LogAsync("Şifre Sıfırlandı (Admin)", user.Id, user.Email);

        return RedirectToAction(nameof(Index));
    }
}