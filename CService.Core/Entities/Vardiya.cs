using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using CService.Core.Constants.Enums;
using CService.Core.Extensions;
using CService.Core.Helpers;

namespace CService.Core.Entities;

public class Vardiya : BaseEntity
{
    [Required]
    public string Kod { get; set; } = string.Empty;

    [Required]
    public string Adi { get; set; } = string.Empty;

    public enmVardiyaTipi Tip { get; set; }

    public TimeOnly Saat { get; set; }

    [NotMapped]
    public string Display => $"{Saat:HH:mm} - {EnumHelper.GetEnumDescription(Tip)}";
}