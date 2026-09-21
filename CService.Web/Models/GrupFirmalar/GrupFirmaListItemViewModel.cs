using CService.Core.Entities;

namespace CService.Web.Models.GrupFirmalar;

public class GrupFirmaListItemViewModel
{
    public GrupFirma GrupFirma { get; set; } = null!;
    public List<string> UyeFirmalar { get; set; } = new();
}