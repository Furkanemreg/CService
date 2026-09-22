using CService.Web.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CService.Core.Entities;
using CService.Core.Interfaces;

namespace CService.Web.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IBaseService<GlobalSetting> _globalSettingService;
    private readonly IActivityLogger _activityLogger;

    public AccountController(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IBaseService<GlobalSetting> globalSettingService,
        IActivityLogger activityLogger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _globalSettingService = globalSettingService;
        _activityLogger = activityLogger;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult OpeningPassword(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OpeningPassword(string password, string? returnUrl)
    {
        var setting = (await _globalSettingService.GetAllAsync()).FirstOrDefault();

        if (setting is null || password != setting.OpeningPassword)
        {
            ModelState.AddModelError(string.Empty, "Şifre hatalı. Tekrar deneyin veya yöneticinizle iletişime geçin.");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        Response.Cookies.Append("OpeningPasswordVerified", "true", new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddHours(12),
            IsEssential = true
        });

        return LocalRedirect(string.IsNullOrEmpty(returnUrl) ? Url.Action("Index", "Home")! : returnUrl);
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is not null && user.IsDeleted)
        {
            ModelState.AddModelError(string.Empty, "Bu hesap silinmiş. Yöneticinizle iletişime geçin.");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(
            model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

        if (result.Succeeded)
            return RedirectToLocal(returnUrl);

        if (result.IsLockedOut)
        {
            ModelState.AddModelError(string.Empty, "Hesabınız kısıtlanmış. Yöneticinizle iletişime geçin.");
            return View(model);
        }

        ModelState.AddModelError(string.Empty, "E-posta veya şifre hatalı.");
        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult ChangePassword() => View(new ChangePasswordViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Challenge();

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(model);
        }

        // Şifre değişince security stamp yenilenir; oturumu tazelemezsek kullanıcı anında dışarı atılır.
        await _signInManager.RefreshSignInAsync(user);
        await _activityLogger.LogAsync("Kendi Şifresini Değiştirdi", user.Id, user.Email);

        ViewBag.Success = true;
        return View(model);
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index", "Home");
    }
}