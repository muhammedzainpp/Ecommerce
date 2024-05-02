using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Web.Domain.ValueObjects;
[ComplexType]
public record Money
{
    public required Currency Currency { get; init; }
    public required decimal Amount { get; init; }
    public static Money Create(string currencyName, string currencySymbol, decimal amount) =>
        new() { Currency = Currency.Create(currencyName, currencySymbol), Amount = amount };
}
