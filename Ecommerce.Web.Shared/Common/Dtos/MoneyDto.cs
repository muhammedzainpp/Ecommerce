namespace Ecommerce.Web.Shared.Common.Dtos;
public class MoneyDto
{
    public CurrencyDto Currency { get; set; } =  new CurrencyDto();
    public decimal Amount { get; set; }
}
