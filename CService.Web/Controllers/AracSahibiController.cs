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
public class AracSahibiController : Controller
{
    private readonly IBaseService<AracSahibi> _service;
    private readonly IBaseService<BankaHesabi> _bankaHesabiService;
    private readonly IBaseService<OdemeGrubu> _odemeGrubuService;

    public AracSahibiController(
        IBaseService<AracSahibi> service,
        IBaseService<BankaHesabi> bankaHesabiService,
        IBaseService<OdemeGrubu> odemeGrubuService)
    {
        _service = service;
        _bankaHesabiService = bankaHesabiService;
        _odemeGrubuService = odemeGrubuService;
    }

    public async Task<IActionResult> Index() => View(await _service.GetAllAsync());

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
    public async Task<IActionResult> Edit(int id, AracSahibi model)
    {
        if (id != model.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync();
            return View(model);
        }

        var existing = await _service.GetByIdAsync(id);
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
        existing.OdemeTipi = model.OdemeTipi;
        existing.VergiUsulu = model.VergiUsulu;
        existing.BankaHesabiId = model.BankaHesabiId;
        existing.HesapSahibi = model.HesapSahibi;
        existing.HesapNo = model.HesapNo;
        existing.Iban = model.Iban;
        existing.YakitOrani = model.YakitOrani;
        existing.OkulKomisyonu = model.OkulKomisyonu;
        existing.OdemeGrubuId = model.OdemeGrubuId;

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
        ViewBag.OdemeTipleri = Enum.GetValues<enmOdemeTipi>()
            .Select(v => new SelectListItem { Value = ((int)v).ToString(), Text = EnumHelper.GetEnumDescription(v) })
            .ToList();

        ViewBag.VergiUsulleri = Enum.GetValues<enmVergiUsulu>()
            .Select(v => new SelectListItem { Value = ((int)v).ToString(), Text = EnumHelper.GetEnumDescription(v) })
            .ToList();

        ViewBag.BankaHesaplari = new SelectList(await _bankaHesabiService.GetAllAsync(), nameof(BankaHesabi.Id), nameof(BankaHesabi.BankaAdi));
        ViewBag.OdemeGruplari = new SelectList(await _odemeGrubuService.GetAllAsync(), nameof(OdemeGrubu.Id), nameof(OdemeGrubu.Ad));
    }
}