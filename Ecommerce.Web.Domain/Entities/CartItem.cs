using Ecommerce.Web.Domain.Entities.Base;
using Ecommerce.Web.Domain.ValueObjects;
using System.Numerics;

namespace Ecommerce.Web.Domain.Entities;
public class CartItem : EntityBase
{
    public Cart Cart { get; set; } = default!;
    public int CartId { get; set; }
    public Product Product { get; set; } = default!;
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public Money TotalPrice { get; set; } = default!;
}
