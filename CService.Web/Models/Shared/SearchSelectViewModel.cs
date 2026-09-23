namespace CService.Web.Models.Shared;

public class SearchSelectViewModel
{
    public string HiddenName { get; set; } = string.Empty;
    public string SearchUrl { get; set; } = string.Empty;
    public string Placeholder { get; set; } = string.Empty;
    public int? SelectedId { get; set; }
    public string? SelectedText { get; set; }
}