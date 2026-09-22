using CService.Core.Entities.General;
using CService.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CService.Core.Data.Seeders;

public static class WitholdingRateSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var unitOfWork = services.GetRequiredService<IUnitOfWork>();
        var repo = unitOfWork.Repository<WitholdingRate>();

        if ((await repo.GetAllAsync()).Any()) return;

        var rates = new[]
        {
            new WitholdingRate { Code = "601", Description = "Yapım İşleri İle Bu İşlerle Birlikte İfa Edilen Mühendislik-Mimarlık Ve Etüt-Proje Hizmetleri", Rate = 4 },
            new WitholdingRate { Code = "602", Description = "Etüt, Plan-Proje, Danışmanlık, Denetim Ve Benzeri Hizmetler", Rate = 9 },
            new WitholdingRate { Code = "603", Description = "Makine, Teçhizat, Demirbaş Ve Taşıtlara Ait Tadil, Bakım Ve Onarım Hizmetleri", Rate = 7 },
            new WitholdingRate { Code = "604", Description = "Yemek Servis Hizmeti", Rate = 5 },
            new WitholdingRate { Code = "605", Description = "Organizasyon Hizmeti", Rate = 5 },
            new WitholdingRate { Code = "606", Description = "İşgücü Temin Hizmetleri", Rate = 9 },
            new WitholdingRate { Code = "607", Description = "Özel Güvenlik Hizmeti", Rate = 9 },
            new WitholdingRate { Code = "608", Description = "Yapı Denetim Hizmetleri", Rate = 9 },
            new WitholdingRate { Code = "609", Description = "Fason Olarak Yaptırılan Tekstil Ve Konfeksiyon İşleri, Çanta Ve Ayakkabı Dikim İşleri Ve Bu İşlere Aracılık Hizmetleri", Rate = 7 },
            new WitholdingRate { Code = "610", Description = "Turistik Mağazalara Verilen Müşteri Bulma / Götürme Hizmetleri", Rate = 9 },
            new WitholdingRate { Code = "611", Description = "Spor Kulüplerinin Yayın, Reklam Ve İsim Hakkı Gelirlerine Konu İşlemleri", Rate = 9 },
            new WitholdingRate { Code = "612", Description = "Temizlik Hizmeti", Rate = 9 },
            new WitholdingRate { Code = "613", Description = "Çevre Ve Bahçe Bakım Hizmetleri", Rate = 9 },
            new WitholdingRate { Code = "614", Description = "Servis Taşımacılığı Hizmeti", Rate = 5 },
            new WitholdingRate { Code = "615", Description = "Her Türlü Baskı Ve Basım Hizmetleri", Rate = 7 },
            new WitholdingRate { Code = "616", Description = "Diğer Hizmetler [Kdvgut-(I/C-2.1.3.2.13)]", Rate = 5 },
            new WitholdingRate { Code = "617", Description = "Hurda Metalden Elde Edilen Külçe Teslimleri", Rate = 7 },
            new WitholdingRate { Code = "618", Description = "Hurda Metalden Elde Edilenler Dışındaki Bakır, Çinko, Demir, Çelik, Alüminyum Ve Kurşun Külçe Teslimleri [Kdvgut-(I/C-2.1.3.3.1)]", Rate = 7 },
            new WitholdingRate { Code = "619", Description = "Bakır, Çinko Ve Alüminyum Ürünlerinin Teslimi", Rate = 7 },
            new WitholdingRate { Code = "620", Description = "İstisnadan Vazgeçenlerin Hurda Ve Atık Teslimi", Rate = 7 },
            new WitholdingRate { Code = "621", Description = "Metal, Plastik, Lastik, Kauçuk, Kağıt Ve Cam Hurda Ve Atıklardan Elde Edilen Hammadde Teslimi", Rate = 9 },
            new WitholdingRate { Code = "622", Description = "Pamuk, Tiftik, Yün Ve Yapağı İle Ham Post Ve Deri Teslimleri", Rate = 9 },
            new WitholdingRate { Code = "623", Description = "Ağaç Ve Orman Ürünleri Teslimi", Rate = 5 },
            new WitholdingRate { Code = "624", Description = "Yük Taşımacılığı Hizmeti [Kdvgut-(I/C-2.1.3.2.11)]", Rate = 2 },
            new WitholdingRate { Code = "625", Description = "Ticari Reklam Hizmetleri [Kdvgut-(I/C-2.1.3.2.15)]", Rate = 3 },
            new WitholdingRate { Code = "626", Description = "Diğer Teslimler [Kdvgut-(I/C-2.1.3.3.7.)]", Rate = 2 },
            new WitholdingRate { Code = "627", Description = "Demir-Çelik Ürünlerinin Teslimi [Kdvgut-(I/C-2.1.3.3.8)]", Rate = 5 },

            new WitholdingRate { Code = "801", Description = "Yapım İşleri ile Bu İşlerle Birlikte İfa Edilen Mühendislik-Mimarlık ve Etüt-Proje Hizmetleri[KDVGUT-(I/C-2.1.3.2.1)]", Rate = 10 },
            new WitholdingRate { Code = "802", Description = "Etüt, Plan-Proje, Danışmanlık, Denetim ve Benzeri Hizmetler[KDVGUT-(I/C-2.1.3.2.2)]", Rate = 10 },
            new WitholdingRate { Code = "803", Description = "Makine, Teçhizat, Demirbaş ve Taşıtlara Ait Tadil, Bakım ve Onarım Hizmetleri[KDVGUT-(I/C-2.1.3.2.3)]", Rate = 10 },
            new WitholdingRate { Code = "804", Description = "Yemek Servis Hizmeti[KDVGUT-(I/C-2.1.3.2.4)]", Rate = 10 },
            new WitholdingRate { Code = "805", Description = "Organizasyon Hizmeti[KDVGUT-(I/C-2.1.3.2.4)]", Rate = 10 },
            new WitholdingRate { Code = "806", Description = "İşgücü Temin Hizmetleri[KDVGUT-(I/C-2.1.3.2.5)]", Rate = 10 },
            new WitholdingRate { Code = "807", Description = "Özel Güvenlik Hizmeti[KDVGUT-(I/C-2.1.3.2.5)]", Rate = 10 },
            new WitholdingRate { Code = "808", Description = "Yapı Denetim Hizmetleri[KDVGUT-(I/C-2.1.3.2.6)]", Rate = 10 },
            new WitholdingRate { Code = "809", Description = "Fason Olarak Yaptırılan Tekstil ve Konfeksiyon İşleri, Çanta ve Ayakkabı Dikim İşleri ve Bu İşlere Aracılık Hizmetleri[KDVGUT-(I/C-2.1.3.2.7)]", Rate = 10 },
            new WitholdingRate { Code = "810", Description = "Turistik Mağazalara Verilen Müşteri Bulma/ Götürme Hizmetleri[KDVGUT-(I/C-2.1.3.2.8)]", Rate = 10 },
            new WitholdingRate { Code = "811", Description = "Spor Kulüplerinin Yayın, Reklâm ve İsim Hakkı Gelirlerine Konu İşlemleri[KDVGUT-(I/C-2.1.3.2.9)]", Rate = 10 },
            new WitholdingRate { Code = "812", Description = "Temizlik Hizmeti[KDVGUT-(I/C-2.1.3.2.10)]", Rate = 10 },
            new WitholdingRate { Code = "813", Description = "Çevre ve Bahçe Bakım Hizmetleri[KDVGUT-(I/C-2.1.3.2.10)]", Rate = 10 },
            new WitholdingRate { Code = "814", Description = "Servis Taşımacılığı Hizmeti[KDVGUT-(I/C-2.1.3.2.11)]", Rate = 10 },
            new WitholdingRate { Code = "815", Description = "Her Türlü Baskı ve Basım Hizmetleri[KDVGUT-(I/C-2.1.3.2.12)]", Rate = 10 },
            new WitholdingRate { Code = "816", Description = "Hurda Metalden Elde Edilen Külçe Teslimleri[KDVGUT-(I/C-2.1.3.3.1)]", Rate = 10 },
            new WitholdingRate { Code = "817", Description = "Hurda Metalden Elde Edilenler Dışındaki Bakır, Çinko, Demir Çelik, Alüminyum ve Kurşun Külçe Teslimi [KDVGUT-(I/C-2.1.3.3.1)]", Rate = 10 },
            new WitholdingRate { Code = "818", Description = "Bakır, Çinko, Alüminyum ve Kurşun Ürünlerinin Teslimi[KDVGUT-(I/C-2.1.3.3.2)]", Rate = 10 },
            new WitholdingRate { Code = "819", Description = "İstisnadan Vazgeçenlerin Hurda ve Atık Teslimi[KDVGUT-(I/C-2.1.3.3.3)]", Rate = 10 },
            new WitholdingRate { Code = "820", Description = "Metal, Plastik, Lastik, Kauçuk, Kâğıt ve Cam Hurda ve Atıklardan Elde Edilen Hammadde Teslimi[KDVGUT-(I/C-2.1.3.3.4)]", Rate = 10 },
            new WitholdingRate { Code = "821", Description = "Pamuk, Tiftik, Yün ve Yapağı İle Ham Post ve Deri Teslimleri[KDVGUT-(I/C-2.1.3.3.5)]", Rate = 10 },
            new WitholdingRate { Code = "822", Description = "Ağaç ve Orman Ürünleri Teslimi[KDVGUT-(I/C-2.1.3.3.6)]", Rate = 10 },
            new WitholdingRate { Code = "823", Description = "Yük Taşımacılığı Hizmeti [KDVGUT-(I/C-2.1.3.2.11)]", Rate = 10 },
            new WitholdingRate { Code = "824", Description = "Ticari Reklam Hizmetleri [KDVGUT-(I/C-2.1.3.2.15)]", Rate = 10 },
            new WitholdingRate { Code = "825", Description = "Demir-Çelik Ürünlerinin Teslimi [KDVGUT-(I/C-2.1.3.3.8)]", Rate = 10 },
        };

        foreach (var rate in rates)
            await repo.AddAsync(rate);

        await unitOfWork.SaveChangesAsync();
    }
}