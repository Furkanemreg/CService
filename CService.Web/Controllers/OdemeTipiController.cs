using CService.Core.Constants.Enums;
using CService.Core.Entities;
using CService.Core.Extensions;
using CService.Core.Helpers;
using CService.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CService.Web.Controllers;

[Authorize(Roles = "Admin,Manager")]
public class OdemeTipiController : Controller
{
    private readonly IBaseService<OdemeTipi> _service;

    public OdemeTipiController(IBaseService<OdemeTipi> service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index() => View(await _service.GetAllAsync());

    [HttpGet]
    public IActionResult Create()
    {
        PopulateKdvDropdown();
        return View(new OdemeTipi());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OdemeTipi model)
    {
        if (!ModelState.IsValid)
        {
            PopulateKdvDropdown();
            return View(model);
        }

        await _service.CreateAsync(model);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item is null) return NotFound();

        PopulateKdvDropdown();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, OdemeTipi model)
    {
        if (id != model.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            PopulateKdvDropdown();
            return View(model);
        }

        var existing = await _service.GetByIdAsync(id);
        if (existing is null) return NotFound();

        existing.Kod = model.Kod;
        existing.Ad = model.Ad;
        existing.Kdv = model.Kdv;
        await _service.UpdateAsync(existing);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private void PopulateKdvDropdown()
    {
        ViewBag.KdvDurumlari = Enum.GetValues<enmKdvDurumu>()
            .Select(v => new SelectListItem { Value = ((int)v).ToString(), Text = EnumHelper.GetEnumDescription(v) })
            .ToList();
    }
}