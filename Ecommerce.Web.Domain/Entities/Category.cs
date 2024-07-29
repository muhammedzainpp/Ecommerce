using Ecommerce.Web.Domain.Entities.Base;

namespace Ecommerce.Web.Domain.Entities;
public class Category : EntityBase
{
    public string Name { get; private set; } = default!;
    public Category? ParentCategory { get; set; }
    public int? ParentCategoryId { get; set; }
    public string? ImageUrl { get;private set; }
    public static Category Create(int? parentCategoryId,string name,string imageUrl) => new Category
    {
        Name = name,
        ParentCategoryId = parentCategoryId,
        ImageUrl = imageUrl
    };
}
