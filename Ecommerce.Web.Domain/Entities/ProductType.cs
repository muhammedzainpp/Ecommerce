using Ecommerce.Web.Domain.Entities.Base;

namespace Ecommerce.Web.Domain.Entities;
public class ProductType : EntityBase
{
    public string Name { get; private set; } = default!;

    public static ProductType Create(string name) => new ProductType
    {
        Name = name
    };
}
