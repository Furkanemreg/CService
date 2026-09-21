using System.ComponentModel;
namespace CService.Core.Constants.Enums;
public enum enmAccountRoles
{
    [Description("Yönetici")]
    Admin = 1,

    [Description("Müdür")]
    Manager = 2,

    [Description("Kullanıcı")]
    User = 3
}

public enum enmOkulOdemeTpi
{
    [Description("Komisyonlu Sözleşme")]
    KomisyonluSozlesme = 1,

    [Description("Günlük Sözleşme")]
    GunlukSozlesme = 2,

    [Description("Aylık Sözleşme")]
    AylikSozlesme = 3
}

public enum enmOdemeTipi
{
    [Description("Elden Ödeme")]
    EldenOdeme = 1,

    [Description("Banka Havalesi")]
    BankaHavalesi = 2,

    [Description("Ödeme Yapma")]
    OdemeYapma = 3
}

public enum enmVergiUsulu
{
    [Description("Basit Usul")]
    BasitUsul = 1,

    [Description("KDV")]
    KDV = 2
}

public enum enmOdemeDurumu
{
    [Description("Açık")]
    Acik = 1,

    [Description("Kapalı")]
    Kapali = 2
}