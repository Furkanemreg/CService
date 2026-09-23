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
public class VardiyaController : Controller
{
    private readonly IBaseService<Vardiya> _service;

    public VardiyaController(IBaseService<Vardiya> service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index() => View(await _service.GetAllAsync());

    [HttpGet]
    public IActionResult Create()
    {
        PopulateDropdowns();
        return View(new Vardiya());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Vardiya model)
    {
        if (!ModelState.IsValid)
        {
            PopulateDropdowns();
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

        PopulateDropdowns();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Vardiya model)
    {
        if (id != model.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            PopulateDropdowns();
            return View(model);
        }

        var existing = await _service.GetByIdAsync(id);
        if (existing is null) return NotFound();

        existing.Kod = model.Kod;
        existing.Adi = model.Adi;
        existing.Tip = model.Tip;
        existing.Saat = model.Saat;
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

    private void PopulateDropdowns()
    {
        ViewBag.Tipler = Enum.GetValues<enmVardiyaTipi>()
            .Select(v => new SelectListItem { Value = ((int)v).ToString(), Text = EnumHelper.GetEnumDescription(v) })
            .ToList();
    }
}