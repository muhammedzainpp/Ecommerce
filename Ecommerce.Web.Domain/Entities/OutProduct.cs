using Ecommerce.Web.Domain.Entities.Base;
using Ecommerce.Web.Domain.ValueObjects;

namespace Ecommerce.Web.Domain.Entities;
public class OutProduct : EntityBase
{
    public Product Product { get; private set; } = default!;
    public int ProductId { get; set; }
    public string ImageUrl { get; private set; } = default!;
    public Money Price { get; private set; } = default!;
    public static OutProduct Create(int productId,Money price,string imageUrl)
    {
        return new OutProduct()
        {
            ProductId = productId,
            Price = price,
            ImageUrl = imageUrl
        };
    }


}
