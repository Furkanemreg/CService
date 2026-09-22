using CService.Core.Constants.Enums;
using CService.Core.Entities;
using CService.Core.Extensions;
using CService.Core.Helpers;
using CService.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CService.Web.Controllers;

[Authorize(Roles = "Admin,Manager")]
public class AracSahibiController : Controller
{
    private readonly IBaseService<AracSahibi> _baseService;
    private readonly IBaseService<BankaHesabi> _bankaHesabiService;
    private readonly IBaseService<OdemeGrubu> _odemeGrubuService;
    private readonly IBaseService<Arac> _aracService;
    private readonly IBaseService<OdemeTipi> _odemeTipiService;

    public AracSahibiController(
        IBaseService<AracSahibi> baseService,
        IBaseService<BankaHesabi> bankaHesabiService,
        IBaseService<OdemeGrubu> odemeGrubuService,
        IBaseService<Arac> aracService,
        IBaseService<OdemeTipi> odemeTipiService)
    {
        _baseService = baseService;
        _bankaHesabiService = bankaHesabiService;
        _odemeGrubuService = odemeGrubuService;
        _aracService = aracService;
        _odemeTipiService = odemeTipiService;
    }

    private async Task PopulateDropdownsAsync()
    {
        ViewBag.OdemeTipleri = new SelectList(await _odemeTipiService.GetAllAsync(), nameof(OdemeTipi.Id), nameof(OdemeTipi.Ad));

        ViewBag.VergiUsulleri = Enum.GetValues<enmVergiUsulu>()
            .Select(v => new SelectListItem { Value = ((int)v).ToString(), Text = EnumHelper.GetEnumDescription(v) })
            .ToList();

        ViewBag.BankaHesaplari = new SelectList(await _bankaHesabiService.GetAllAsync(), nameof(BankaHesabi.Id), nameof(BankaHesabi.BankaAdi));
        ViewBag.OdemeGruplari = new SelectList(await _odemeGrubuService.GetAllAsync(), nameof(OdemeGrubu.Id), nameof(OdemeGrubu.Ad));
    }

    public async Task<IActionResult> Index() => View(await _baseService.GetAllAsync());

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await PopulateDropdownsAsync();
        return View(new AracSahibi { IsActive = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AracSahibi model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync();
            return View(model);
        }

        await _baseService.CreateAsync(model);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _baseService.GetByIdAsync(id);
        if (item is null) return NotFound();

        await PopulateDropdownsAsync();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AracSahibi model)
    {
        if (id != model.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync();
            return View(model);
        }

        var existing = await _baseService.GetByIdAsync(id);
        if (existing is null) return NotFound();

        existing.Kod = model.Kod;
        existing.Adi = model.Adi;
        existing.Soyad = model.Soyad;
        existing.TcKimlikNo = model.TcKimlikNo;
        existing.Tel = model.Tel;
        existing.Adres = model.Adres;
        existing.VergiDairesi = model.VergiDairesi;
        existing.VergiNo = model.VergiNo;
        existing.Vekil = model.Vekil;
        existing.VekilTel = model.VekilTel;
        existing.Not = model.Not;
        existing.Sorumlu = model.Sorumlu;
        existing.IsActive = model.IsActive;
        existing.OdemeTipiId = model.OdemeTipiId;
        existing.VergiUsulu = model.VergiUsulu;
        existing.BankaHesabiId = model.BankaHesabiId;
        existing.HesapSahibi = model.HesapSahibi;
        existing.HesapNo = model.HesapNo;
        existing.Iban = model.Iban;
        existing.YakitOrani = model.YakitOrani;
        existing.OkulKomisyonu = model.OkulKomisyonu;
        existing.OdemeGrubuId = model.OdemeGrubuId;

        await _baseService.UpdateAsync(existing);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _baseService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> GetVehicles(int aracSahibiId)
    {
        var items = await _aracService.Query()
            .Where(a => a.AracSahibiId == aracSahibiId)
            .Include(a => a.Firma)
            .Include(a => a.AracCinsi)
            .Include(a => a.AracTipi)
            .Include(a => a.AracMarka)
            .ToListAsync();

        return PartialView("_VehiclesPartial", items);
    }
}