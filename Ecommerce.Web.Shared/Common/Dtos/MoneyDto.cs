namespace Ecommerce.Web.Shared.Common.Dtos;
public class MoneyDto
{
    public CurrencyDto Currency { get; set; } =  new CurrencyDto() { Name =string.Empty,Symbol = string.Empty};
    public decimal Amount { get; set; }

}
