using Ecommerce.Web.Client.Services.Carts.Dtos;
using Ecommerce.Web.Shared.Reponses;

namespace Ecommerce.Web.Client.Services.Carts;

public interface ICartServices
{
    Func<AddCartItemDto, Task>? OnButtonClicked { get; set; }

    Task<Response<int>> AddToCart(AddCartItemDto request);
    Task<IEnumerable<GetCartItemDto>> GetAllCartItems(int userId);
    Task RaiseEvent(AddCartItemDto request);
    Task SetTotalItemCount(AddCartItemDto request);
}
