using Ecommerce.Web.Domain.Entities.Base;
using Ecommerce.Web.Domain.ValueObjects;

namespace Ecommerce.Web.Domain.Entities;
public class Item : EntityBase
{
    public Product Product { get; private set; } = default!;
    public int ProductId { get;private set; }
    public string ImageUrl { get; private set; } = default!;
    public Money Price { get; private set; } = default!;
    public int Quantity { get;private set; }
    public static Item Create(int productId,Money price,string imageUrl,int quantity)
    {
        return new Item()
        {
            ProductId = productId,
            Price = price,
            ImageUrl = imageUrl,
            Quantity = quantity
        };
    }

    public static Item Update(Item item,int productId, Money price, string imageUrl, int quantity)
    {
        item.ProductId = productId;
        item.Price = price;
        item.ImageUrl = imageUrl;
        item.Quantity = quantity;

        return item;
    
    }
}
