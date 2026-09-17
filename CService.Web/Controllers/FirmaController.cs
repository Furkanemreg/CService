using CService.Core.Entities;
using CService.Core.Entities.General;
using CService.Core.Interfaces;
using CService.Core.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WitholdingRate = CService.Core.Entities.General.WitholdingRate;

namespace CService.Web.Controllers;

[Authorize(Roles = "Admin,Manager")]
public class FirmaController : Controller
{
    private readonly IBaseService<Firma> _service;
    private readonly IBaseService<Bolge> _bolgeService;
    private readonly IBaseService<GrupFirma> _grupFirmaService;
    private readonly IBaseService<VatRate> _vatRateService;
    private readonly IBaseService<WitholdingRate> _tevkifatRateService;
    private readonly IBaseService<BankaHesabi> _bankaHesabiService;

    public FirmaController(
        IBaseService<Firma> service,
        IBaseService<Bolge> bolgeService,
        IBaseService<GrupFirma> grupFirmaService,
        IBaseService<VatRate> vatRateService,
        IBaseService<WitholdingRate> tevkifatRateService,
        IBaseService<BankaHesabi> bankaHesabiService)
    {
        _service = service;
        _bolgeService = bolgeService;
        _grupFirmaService = grupFirmaService;
        _vatRateService = vatRateService;
        _tevkifatRateService = tevkifatRateService;
        _bankaHesabiService = bankaHesabiService;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _service.Query()
            .Include(f => f.Bolge)
            .Include(f => f.GrupFirma)
            .ToListAsync();

        return View(items);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await PopulateDropdownsAsync();
        return View(new Firma { IsActive = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Firma model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync();
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

        await PopulateDropdownsAsync();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Firma model)
    {
        if (id != model.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync();
            return View(model);
        }

        var existing = await _service.GetByIdAsync(id);
        if (existing is null) return NotFound();

        existing.FirmaKodu = model.FirmaKodu;
        existing.Adi = model.Adi;
        existing.VergiNo = model.VergiNo;
        existing.VergiDairesi = model.VergiDairesi;
        existing.Tel1 = model.Tel1;
        existing.Tel2 = model.Tel2;
        existing.Mail = model.Mail;
        existing.Not = model.Not;
        existing.IsActive = model.IsActive;
        existing.BolgeId = model.BolgeId;
        existing.GrupFirmaId = model.GrupFirmaId;
        existing.VatRateId = model.VatRateId;
        existing.WitholdingRateId = model.WitholdingRateId;
        existing.OkulServisi = model.OkulServisi;
        existing.HavaleBankaHesabiId = model.HavaleBankaHesabiId;
        existing.KrediKartiBankaHesabiId = model.KrediKartiBankaHesabiId;
        existing.Unvan = model.Unvan;
        existing.Adres = model.Adres;

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

    private async Task PopulateDropdownsAsync()
    {
        ViewBag.Bolgeler = new SelectList(await _bolgeService.GetAllAsync(), nameof(Bolge.Id), nameof(Bolge.Ad));
        ViewBag.GrupFirmalar = new SelectList(await _grupFirmaService.GetAllAsync(), nameof(GrupFirma.Id), nameof(GrupFirma.Ad));

        var vatRates = (await _vatRateService.GetAllAsync()).OrderBy(x => x.Rate).ToList();
        ViewBag.VatRates = new SelectList(vatRates, nameof(VatRate.Id), nameof(VatRate.Description));

        var tevkifatRates = (await _tevkifatRateService.GetAllAsync()).OrderBy(x => x.Rate).ToList();
        ViewBag.TevkifatRates = new SelectList(tevkifatRates, nameof(WitholdingRate.Id), nameof(WitholdingRate.Display));

        ViewBag.BankaHesaplari = new SelectList(await _bankaHesabiService.GetAllAsync(), nameof(BankaHesabi.Id), nameof(BankaHesabi.BankaAdi));
    }
}