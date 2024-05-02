namespace Ecommerce.Web.Shared.Common.Dtos;
public class MoneyDto
{
    public CurrencyDto Currency { get; init; } = default!;
    public  decimal Amount { get; init; }
}
