using Ecommerce.Web.Domain.Entities.Base;

namespace Ecommerce.Web.Domain.Entities;
public class ProductVariant : EntityBase
{
    public Product Product { get; private set; } = default!;
    public int ProductId { get; private set; }
    public ProductType ProductType { get; private set; } = default!;
    public int ProductTypeId { get;private set; }


    public static ProductVariant Create(int productId , int productTypeId) => new ProductVariant
    {
       ProductId = productId ,
       ProductTypeId = productTypeId
    };
}


