using Ecommerce.Web.Domain.Entities.Base;

namespace Ecommerce.Web.Domain.Entities;
public class AppSetting : EntityBase
{
    public string Country { get;  set; } = default!;
    public string CurrencyName { get;  set; } = default!;
    public string CurrencySymbol { get;  set; } = default!;

    public static AppSetting Update(AppSetting appsetting , string country, string currencyName , string currencySymbol)
    {

        appsetting.Country = country;
        appsetting.CurrencyName = currencyName;
        appsetting.CurrencySymbol = currencySymbol;

        return appsetting;
    }
}

