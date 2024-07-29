namespace Ecommerce.Web.Client.Services.AppSettings.Dtos;

public class AppSettingsDto
{
    public string Country { get; set; } = default!; 
    public  string CurrencyName { get; set; } = default!;
    public  string CurrencySymbol { get; set; } = default!;
}
