using Ecommerce.Web.Client.Services.Carts.Dtos;
using Ecommerce.Web.Shared.Reponses;

namespace Ecommerce.Web.Client.Services.Carts;

public interface ICartServices
{
    Func<CartItemDto, Task>? OnButtonClicked { get; set; }

    Task<Response<int>> AddToCart(CartItemDto request);
    Task RaiseEvent(CartItemDto request);
    Task SetTotalItemCount(CartItemDto request);
}
