using CService.Core.Entities;
using CService.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CService.Web.Controllers;

[Authorize]
public class SharedController : Controller
{
    private readonly IBaseService<Firma> _firmaService;
    private readonly IBaseService<Yetkili> _yetkiliService;
    private readonly IBaseService<Hostes> _hostesService;
    private readonly IBaseService<Bolge> _bolgeService;
    private readonly IBaseService<BankaHesabi> _bankaHesabiService;
    private readonly IBaseService<OdemeTipi> _odemeTipiService;
    private readonly IBaseService<OdemeGrubu> _odemeGrubuService;
    private readonly IBaseService<AracSahibi> _aracSahibiService;

    public SharedController(
        IBaseService<Firma> firmaService,
        IBaseService<Yetkili> yetkiliService,
        IBaseService<Hostes> hostesService,
        IBaseService<Bolge> bolgeService,
        IBaseService<BankaHesabi> bankaHesabiService,
        IBaseService<OdemeTipi> odemeTipiService,
        IBaseService<OdemeGrubu> odemeGrubuService,
        IBaseService<AracSahibi> aracSahibiService)
    {
        _firmaService = firmaService;
        _yetkiliService = yetkiliService;
        _hostesService = hostesService;
        _bolgeService = bolgeService;
        _bankaHesabiService = bankaHesabiService;
        _odemeTipiService = odemeTipiService;
        _odemeGrubuService = odemeGrubuService;
        _aracSahibiService = aracSahibiService;
    }

    [HttpGet] /**/
    public async Task<IActionResult> SearchFirma(string? q)
    {
        var query = _firmaService.Query();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.ToLower();
            query = query.Where(f => f.Adi.ToLower().Contains(term) || f.FirmaKodu.ToLower().Contains(term));
        }

        var results = await query
            .OrderBy(f => f.Adi)
            .Take(20)
            .Select(f => new { id = f.Id, text = f.FirmaKodu + " - " + f.Adi })
            .ToListAsync();

        return Json(results);
    }

    [HttpGet]
    public async Task<IActionResult> SearchAracSahibi(string? q)
    {
        var query = _aracSahibiService.Query();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.ToLower();
            query = query.Where(x => x.Adi.ToLower().Contains(term) || x.Soyad.ToLower().Contains(term) || x.Kod.ToLower().Contains(term));
        }

        var results = await query
            .OrderBy(x => x.Adi)
            .Take(20)
            .Select(x => new { id = x.Id, text = x.Kod + " - " + x.Adi + " " + x.Soyad })
            .ToListAsync();

        return Json(results);
    }

    [HttpGet]
    public async Task<IActionResult> SearchOdemeTipi(string? q)
    {
        var query = _odemeTipiService.Query();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.ToLower();
            query = query.Where(x => x.Ad.ToLower().Contains(term) || x.Kod.ToLower().Contains(term));
        }

        var results = await query
            .OrderBy(x => x.Ad)
            .Take(20)
            .Select(x => new { id = x.Id, text = x.Kod + " - " + x.Ad })
            .ToListAsync();

        return Json(results);
    }

    [HttpGet]
    public async Task<IActionResult> SearchOdemeGrubu(string? q)
    {
        var query = _odemeGrubuService.Query();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.ToLower();
            query = query.Where(x => x.Ad.ToLower().Contains(term) || x.Kod.ToLower().Contains(term));
        }

        var results = await query
            .OrderBy(x => x.Ad)
            .Take(20)
            .Select(x => new { id = x.Id, text = x.Kod + " - " + x.Ad })
            .ToListAsync();

        return Json(results);
    }

    [HttpGet]
    public async Task<IActionResult> SearchYetkili(string? q)
    {
        var query = _yetkiliService.Query();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.ToLower();
            query = query.Where(x => x.AdSoyad.ToLower().Contains(term) || x.Kod.ToLower().Contains(term));
        }

        var results = await query
            .OrderBy(x => x.AdSoyad)
            .Take(20)
            .Select(x => new { id = x.Id, text = x.Kod + " - " + x.AdSoyad })
            .ToListAsync();

        return Json(results);
    }

    [HttpGet]
    public async Task<IActionResult> SearchHostes(string? q)
    {
        var query = _hostesService.Query();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.ToLower();
            query = query.Where(x => x.AdSoyad.ToLower().Contains(term) || x.Kod.ToLower().Contains(term));
        }

        var results = await query
            .OrderBy(x => x.AdSoyad)
            .Take(20)
            .Select(x => new { id = x.Id, text = x.Kod + " - " + x.AdSoyad })
            .ToListAsync();

        return Json(results);
    }

    [HttpGet]
    public async Task<IActionResult> SearchBolge(string? q)
    {
        var query = _bolgeService.Query();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.ToLower();
            query = query.Where(x => x.Ad.ToLower().Contains(term) || x.Kod.ToLower().Contains(term));
        }

        var results = await query
            .OrderBy(x => x.Ad)
            .Take(20)
            .Select(x => new { id = x.Id, text = x.Kod + " - " + x.Ad })
            .ToListAsync();

        return Json(results);
    }

    [HttpGet]
    public async Task<IActionResult> SearchBankaHesabi(string? q)
    {
        var query = _bankaHesabiService.Query();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.ToLower();
            query = query.Where(x => x.BankaAdi.ToLower().Contains(term) || (x.SubeAdi != null && x.SubeAdi.ToLower().Contains(term)));
        }

        var results = await query
            .OrderBy(x => x.BankaAdi)
            .Take(20)
            .Select(x => new { id = x.Id, text = x.BankaAdi + (x.SubeAdi != null ? " - " + x.SubeAdi : "") })
            .ToListAsync();

        return Json(results);
    }
}