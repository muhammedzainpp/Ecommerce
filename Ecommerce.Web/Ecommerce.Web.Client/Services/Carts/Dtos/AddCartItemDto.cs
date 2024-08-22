using Ecommerce.Web.Client.Services.Products.Dtos;
using Ecommerce.Web.Shared.Common.Dtos;
using Ecommerce.Web.Shared.Common.Enums;

namespace Ecommerce.Web.Client.Services.Carts.Dtos;

public class AddCartItemDto
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public required MoneyDto TotalPrice { get; set; }
}
