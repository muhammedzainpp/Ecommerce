using Ecommerce.Web.Client.Services.Products.Dtos;
using Ecommerce.Web.Shared.Common.Dtos;
using Ecommerce.Web.Shared.Common.Enums;

namespace Ecommerce.Web.Client.Services.Carts.Dtos;

public class GetCartItemDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required ProductDto Product { get; set; } = new ProductDto { Cost = new(), Name = string.Empty, Description = string.Empty };
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public required MoneyDto TotalPrice { get; set; }
    public CartActitvity Activity { get; set; }
}
