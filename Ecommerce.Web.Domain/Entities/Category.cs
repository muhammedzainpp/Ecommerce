using Ecommerce.Web.Domain.Entities.Base;

namespace Ecommerce.Web.Domain.Entities;
public class Category : EntityBase
{
    public string Name { get; private set; } = default!;
    public string Url { get; private set; } = default!;
    public static Category Create(string name, string url) => new Category
    {
        Name = name,
        Url = url
    };
}
