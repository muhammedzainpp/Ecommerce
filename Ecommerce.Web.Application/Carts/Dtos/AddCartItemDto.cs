using Ecommerce.Web.Application.Products.Dtos;
using Ecommerce.Web.Shared.Common.Dtos;
using Ecommerce.Web.Shared.Common.Enums;

namespace Ecommerce.Web.Application.Carts.Dtos;
public class AddCartItemDto
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public CartActitvity Activity { get; set; }
}
