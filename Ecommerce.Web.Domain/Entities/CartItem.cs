using Ecommerce.Web.Domain.Entities.Base;

namespace Ecommerce.Web.Domain.Entities;
public class CartItem : EntityBase
{
    public Item Item { get; set; } = default!;
    public int ItemId { get; set; }
    public Cart Cart { get; set; } = default!;
    public int CartId { get; set; }
    public int Quantity { get; set; }
    public Product Product { get; set; } = default!;
    public int ProductId { get; set; }
}
