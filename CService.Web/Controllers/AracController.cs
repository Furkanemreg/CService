using CService.Core.Constants.Enums;
using CService.Core.Entities;
using CService.Core.Extensions;
using CService.Core.Helpers;
using CService.Core.Interfaces;
using CService.Web.Models.Araclar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CService.Web.Controllers;

[Authorize(Roles = "Admin,Manager")]
public class AracController : Controller
{
    private readonly IBaseService<Arac> _service;
    private readonly IBaseService<AracSahibi> _aracSahibiService;
    private readonly IBaseService<Firma> _firmaService;
    private readonly IBaseService<AracCinsi> _cinsiService;
    private readonly IBaseService<AracMarka> _markaService;
    private readonly IBaseService<AracTipi> _tipiService;

    public AracController(
        IBaseService<Arac> service,
        IBaseService<AracSahibi> aracSahibiService,
        IBaseService<Firma> firmaService,
        IBaseService<AracCinsi> cinsiService,
        IBaseService<AracMarka> markaService,
        IBaseService<AracTipi> tipiService)
    {
        _service = service;
        _aracSahibiService = aracSahibiService;
        _firmaService = firmaService;
        _cinsiService = cinsiService;
        _markaService = markaService;
        _tipiService = tipiService;
    }

    private async Task PopulateDropdownsAsync()
    {
        ViewBag.OdemeDurumlari = Enum.GetValues<enmOdemeDurumu>().Select(v => new SelectListItem { Value = ((int)v).ToString(), Text = EnumHelper.GetEnumDescription(v) }).ToList();

        ViewBag.Cinsler = new SelectList(await _cinsiService.GetAllAsync(), nameof(AracCinsi.Id), nameof(AracCinsi.Ad));
        ViewBag.Markalar = new SelectList(await _markaService.GetAllAsync(), nameof(AracMarka.Id), nameof(AracMarka.Ad));
        ViewBag.Tipler = new SelectList(await _tipiService.GetAllAsync(), nameof(AracTipi.Id), nameof(AracTipi.Ad));

        ViewBag.AracSahipleri = new SelectList(await _aracSahibiService.GetAllAsync(), nameof(AracSahibi.Id), nameof(AracSahibi.TamAdi));
        ViewBag.Firmalar = new SelectList(await _firmaService.GetAllAsync(), nameof(Firma.Id), nameof(Firma.Adi));
    }

    public async Task<IActionResult> Index()
    {
        var items = await _service.Query()
            .Include(a => a.AracSahibi)
            .Include(a => a.Firma)
            .Include(a => a.AracCinsi)
            .Include(a => a.AracMarka)
            .ToListAsync();

        return View(items);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await PopulateDropdownsAsync();
        return View(new AracCreateViewModel { IsActive = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AracCreateViewModel model)
    {
        if (model.ExistingAracSahibiId is null)
        {
            if (string.IsNullOrWhiteSpace(model.YeniSahibiKod) || string.IsNullOrWhiteSpace(model.YeniSahibiAdi))
                ModelState.AddModelError(string.Empty, "Var olan bir araç sahibi seçmelisiniz ya da yeni sahip için Kod ve Adı girmelisiniz.");
        }

        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync();
            return View(model);
        }

        int aracSahibiId;

        if (model.ExistingAracSahibiId is int existingId)
        {
            aracSahibiId = existingId;
        }
        else
        {
            var yeniSahip = new AracSahibi
            {
                Kod = model.YeniSahibiKod!,
                Adi = model.YeniSahibiAdi!,
                Soyad = model.YeniSahibiSoyad ?? string.Empty,
                TcKimlikNo = model.YeniSahibiTcKimlikNo,
                Tel = model.YeniSahibiTel
            };

            var created = await _aracSahibiService.CreateAsync(yeniSahip);
            aracSahibiId = created.Id;
        }

        var arac = new Arac
        {
            FirmaId = model.FirmaId,
            AracSahibiId = aracSahibiId,
            Plaka = model.Plaka,
            IsActive = model.IsActive,
            OdemeDurumu = model.OdemeDurumu,
            AracCinsiId = model.AracCinsiId,
            AracMarkaId = model.AracMarkaId,
            AracTipiId = model.AracTipiId,
            Modeli = model.Modeli,
            Kapasite = model.Kapasite,
            RuhsatNo = model.RuhsatNo,
            MotorNo = model.MotorNo,
            SasiNo = model.SasiNo,
            Klima = model.Klima,
            IlkGirisTarihi = model.IlkGirisTarihi,
            RuhsatSahibi = model.RuhsatSahibi,
            RuhsatSahibiKimlikNo = model.RuhsatSahibiKimlikNo,
            SoforAdi = model.SoforAdi,
            SoforKimlikNo = model.SoforKimlikNo,
            SoforTelefon = model.SoforTelefon,
            HostesAdi = model.HostesAdi,
            HostesKimlikNo = model.HostesKimlikNo,
            HostesTelefon = model.HostesTelefon
        };

        await _service.CreateAsync(arac);
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
    public async Task<IActionResult> Edit(int id, Arac model)
    {
        if (id != model.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync();
            return View(model);
        }

        var existing = await _service.GetByIdAsync(id);
        if (existing is null) return NotFound();

        existing.FirmaId = model.FirmaId;
        existing.AracSahibiId = model.AracSahibiId;
        existing.Plaka = model.Plaka;
        existing.IsActive = model.IsActive;
        existing.OdemeDurumu = model.OdemeDurumu;
        existing.AracCinsiId = model.AracCinsiId;
        existing.AracMarkaId = model.AracMarkaId;
        existing.AracTipiId = model.AracTipiId;
        existing.Modeli = model.Modeli;
        existing.Kapasite = model.Kapasite;
        existing.RuhsatNo = model.RuhsatNo;
        existing.MotorNo = model.MotorNo;
        existing.SasiNo = model.SasiNo;
        existing.Klima = model.Klima;
        existing.IlkGirisTarihi = model.IlkGirisTarihi;
        existing.RuhsatSahibi = model.RuhsatSahibi;
        existing.RuhsatSahibiKimlikNo = model.RuhsatSahibiKimlikNo;
        existing.SoforAdi = model.SoforAdi;
        existing.SoforKimlikNo = model.SoforKimlikNo;
        existing.SoforTelefon = model.SoforTelefon;
        existing.HostesAdi = model.HostesAdi;
        existing.HostesKimlikNo = model.HostesKimlikNo;
        existing.HostesTelefon = model.HostesTelefon;

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