using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Web.Domain.ValueObjects;
[ComplexType]
public  record Currency
{
    public required string Name { get; init; }
    public required string Symbol { get; init; }
    public static Currency Create(string name, string symbol) =>
        new() { Name = name, Symbol = symbol };
}
