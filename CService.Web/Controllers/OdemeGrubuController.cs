using CService.Core.Entities;
using CService.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CService.Web.Controllers;

[Authorize(Roles = "Admin,Manager")]
public class OdemeGrubuController : Controller
{
    private readonly IBaseService<OdemeGrubu> _service;

    public OdemeGrubuController(IBaseService<OdemeGrubu> service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index() => View(await _service.GetAllAsync());

    [HttpGet]
    public IActionResult Create() => View(new OdemeGrubu());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OdemeGrubu model)
    {
        if (!ModelState.IsValid) return View(model);
        await _service.CreateAsync(model);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound() : View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, OdemeGrubu model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);

        var existing = await _service.GetByIdAsync(id);
        if (existing is null) return NotFound();

        existing.Kod = model.Kod;
        existing.Ad = model.Ad;
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
}