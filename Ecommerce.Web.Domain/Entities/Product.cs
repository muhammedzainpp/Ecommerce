using Ecommerce.Web.Domain.Entities.Base;
using Ecommerce.Web.Domain.ValueObjects;

namespace Ecommerce.Web.Domain.Entities;
public class Product : EntityBase
{
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string ImageUrl { get; private set; } = default!;
    public Money Price { get; private set; } = default!;
    public Category Category { get; private set; } = default!;
    public int CategoryId { get;private set; }

    public static Product Create(string title, string description, string imageUrl, Money price,int categoryId) => new Product
    {
        Title = title,
        Description = description,
        ImageUrl = imageUrl,
        Price = price,
        CategoryId = categoryId
    };
}
