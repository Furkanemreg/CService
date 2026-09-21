using CService.Core.Entities;
using CService.Core.Entities.General;
using CService.Core.Helpers;
using CService.Core.Interfaces;
using CService.Web.Models.GrupFirmalar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CService.Core.Constants.Enums;

namespace CService.Web.Controllers;

[Authorize(Roles = "Admin,Manager")]
public class GrupFirmaController : Controller
{
    private readonly IBaseService<GrupFirma> _service;
    private readonly IBaseService<Firma> _firmaService;
    private readonly IBaseService<Bolge> _bolgeService;
    private readonly IBaseService<VatRate> _vatRateService;
    private readonly IBaseService<WitholdingRate> _tevkifatRateService;
    private readonly IBaseService<BankaHesabi> _bankaHesabiService;

    public GrupFirmaController(
        IBaseService<GrupFirma> service,
        IBaseService<Firma> firmaService,
        IBaseService<Bolge> bolgeService,
        IBaseService<VatRate> vatRateService,
        IBaseService<WitholdingRate> tevkifatRateService,
        IBaseService<BankaHesabi> bankaHesabiService)
    {
        _service = service;
        _firmaService = firmaService;
        _bolgeService = bolgeService;
        _vatRateService = vatRateService;
        _tevkifatRateService = tevkifatRateService;
        _bankaHesabiService = bankaHesabiService;
    }

    public async Task<IActionResult> Index()
    {
        var gruplar = await _service.Query().Include(g => g.Bolge).ToListAsync();
        var firmalar = await _firmaService.GetAllAsync();

        var items = gruplar.Select(g => new GrupFirmaListItemViewModel
        {
            GrupFirma = g,
            UyeFirmalar = firmalar.Where(f => f.GrupFirmaId == g.Id).Select(f => f.Adi).ToList()
        }).ToList();

        return View(items);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await PopulateDropdownsAsync();
        return View(new GrupFirma { IsActive = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GrupFirma model)
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
    public async Task<IActionResult> Edit(int id, GrupFirma model)
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
        existing.Mail = model.Mail;
        existing.Tel1 = model.Tel1;
        existing.Tel2 = model.Tel2;
        existing.BolgeId = model.BolgeId;
        existing.VatRateId = model.VatRateId;
        existing.WitholdingRateId = model.WitholdingRateId;
        existing.HavaleBankaHesabiId = model.HavaleBankaHesabiId;
        existing.KrediKartiBankaHesabiId = model.KrediKartiBankaHesabiId;

        existing.OkulServisi = model.OkulServisi;
        existing.OkulOdemeTpi = model.OkulOdemeTpi;
        if (model.OkulServisi == false)
            existing.OkulOdemeTpi = null;

        existing.Unvan = model.Unvan;
        existing.Adres = model.Adres;
        existing.Not = model.Not;
        existing.IsActive = model.IsActive;

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

        var vatRates = (await _vatRateService.GetAllAsync()).OrderBy(x => x.Rate).ToList();
        ViewBag.VatRates = new SelectList(vatRates, nameof(VatRate.Id), nameof(VatRate.Description));

        var tevkifatRates = (await _tevkifatRateService.GetAllAsync()).OrderBy(x => x.Rate).ToList();
        ViewBag.TevkifatRates = new SelectList(tevkifatRates, nameof(WitholdingRate.Id), nameof(WitholdingRate.Display));

        ViewBag.BankaHesaplari = new SelectList(await _bankaHesabiService.GetAllAsync(), nameof(BankaHesabi.Id), nameof(BankaHesabi.BankaAdi));

        ViewBag.OkulOdemeTipleri = Enum.GetValues<enmOkulOdemeTpi>()
            .Select(v => new SelectListItem
            {
                Value = ((int)v).ToString(),
                Text = EnumHelper.GetEnumDescription(v)
            })
            .ToList();
    }
}