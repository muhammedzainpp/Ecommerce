using Ecommerce.Web.Domain.Entities.Base;
using Ecommerce.Web.Domain.ValueObjects;

namespace Ecommerce.Web.Domain.Entities;
public class Product : EntityBase
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string? ImageUrl { get; private set; } 
    public Category Category { get; private set; } = default!;
    public int CategoryId { get;private set; }
    public Money Cost { get;private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public static Product Create(string name, string description,string? imageUrl,int categoryId,Money cost,int quantity) => new Product
    {
        Name = name,
        Description = description,
        ImageUrl = imageUrl,
        CategoryId = categoryId,
        Cost = cost,
        Quantity = quantity
    };
}
