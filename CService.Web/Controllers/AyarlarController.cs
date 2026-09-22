using CService.Core.Entities;
using CService.Core.Interfaces;
using CService.Web.Models.Ayarlar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CService.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AyarlarController : Controller
{
    private readonly IBaseService<GlobalSetting> _service;

    public AyarlarController(IBaseService<GlobalSetting> service)
    {
        _service = service;
    }

    public IActionResult Index() => View(new OpeningPasswordViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(OpeningPasswordViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var setting = (await _service.GetAllAsync()).FirstOrDefault();

        if (setting is null || model.EskiSifre != setting.OpeningPassword)
        {
            ModelState.AddModelError(nameof(model.EskiSifre), "Eski şifre hatalı.");
            return View(model);
        }

        setting.OpeningPassword = model.YeniSifre;
        await _service.UpdateAsync(setting);

        TempData["Success"] = "Açılış şifresi güncellendi.";
        return View(new OpeningPasswordViewModel());
    }
}