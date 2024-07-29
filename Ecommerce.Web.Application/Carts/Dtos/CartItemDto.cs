using Ecommerce.Web.Shared.Common.Dtos;
using Ecommerce.Web.Shared.Common.Enums;

namespace Ecommerce.Web.Application.Carts.Dtos;
public class CartItemDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public required MoneyDto TotalPrice { get; set; }
    public CartActitvity Activity { get; set; }
}
