using Ecommerce.Web.Domain.Entities.Base;

namespace Ecommerce.Web.Domain.Entities;
public class Product : EntityBase
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string ImageUrl { get; private set; } = default!;
    public Category Category { get; private set; } = default!;
    public int CategoryId { get;private set; }
    public List<Item> OutProducts { get; set; } = default!;
    public static Product Create(string name, string description,string imageUrl,int categoryId) => new Product
    {
        Name = name,
        Description = description,
        ImageUrl = imageUrl,
        CategoryId = categoryId
    };
}
