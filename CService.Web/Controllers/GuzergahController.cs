using CService.Core.Entities;
using CService.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CService.Web.Controllers;

[Authorize(Roles = "Admin,Manager")]
public class GuzergahController : Controller
{
    private readonly IBaseService<Guzergah> _service;
    private readonly IBaseService<Firma> _firmaService;
    private readonly IBaseService<Bolge> _bolgeService;
    private readonly IBaseService<Yetkili> _yetkiliService;
    private readonly IBaseService<Hostes> _hostesService;

    public GuzergahController(
        IBaseService<Guzergah> service,
        IBaseService<Firma> firmaService,
        IBaseService<Bolge> bolgeService,
        IBaseService<Yetkili> yetkiliService,
        IBaseService<Hostes> hostesService)
    {
        _service = service;
        _firmaService = firmaService;
        _bolgeService = bolgeService;
        _yetkiliService = yetkiliService;
        _hostesService = hostesService;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _service.Query()
            .Include(g => g.Firma)
            .Include(g => g.Bolge)
            .ToListAsync();
        return View(items);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View(new Guzergah { Yil = DateTime.Now.Year });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Guzergah model)
    {
        if (!ModelState.IsValid)
        {
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

        await PopulateSelectedDisplaysAsync(item);
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Guzergah model)
    {
        if (id != model.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            await PopulateSelectedDisplaysAsync(model);
            return View(model);
        }

        var existing = await _service.GetByIdAsync(id);
        if (existing is null) return NotFound();

        existing.FirmaId = model.FirmaId;
        existing.Kod = model.Kod;
        existing.Kod2 = model.Kod2;
        existing.Ad = model.Ad;
        existing.BolgeId = model.BolgeId;
        existing.YetkiliId = model.YetkiliId;
        existing.Ay = model.Ay;
        existing.Yil = model.Yil;
        existing.Aciklama = model.Aciklama;
        existing.HostesId = model.HostesId;
        existing.Kapasite = model.Kapasite;
        existing.KmTekYon = model.KmTekYon;
        existing.SeferSayisi = model.SeferSayisi;
        existing.ServisIstikameti = model.ServisIstikameti;

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

    private async Task PopulateSelectedDisplaysAsync(Guzergah model)
    {
        if (model.FirmaId > 0)
        {
            var firma = await _firmaService.GetByIdAsync(model.FirmaId);
            ViewBag.FirmaDisplay = firma is null ? null : $"{firma.FirmaKodu} - {firma.Adi}";
        }

        if (model.BolgeId is int bolgeId)
        {
            var bolge = await _bolgeService.GetByIdAsync(bolgeId);
            ViewBag.BolgeDisplay = bolge is null ? null : $"{bolge.Kod} - {bolge.Ad}";
        }

        if (model.YetkiliId is int yetkiliId)
        {
            var yetkili = await _yetkiliService.GetByIdAsync(yetkiliId);
            ViewBag.YetkiliDisplay = yetkili is null ? null : $"{yetkili.Kod} - {yetkili.AdSoyad}";
        }

        if (model.HostesId is int hostesId)
        {
            var hostes = await _hostesService.GetByIdAsync(hostesId);
            ViewBag.HostesDisplay = hostes is null ? null : $"{hostes.Kod} - {hostes.AdSoyad}";
        }
    }
}