using Ecommerce.Web.Client.Services.Carts.Dtos;
using Ecommerce.Web.Shared.Reponses;

namespace Ecommerce.Web.Client.Services.Carts;

public interface ICartServices
{
    Func<AddCartItemDto, Task>? OnButtonClicked { get; set; }
    int TotalItemCount { get; }

    Task<Response<int>> AddToCart(AddCartItemDto request);
    void DecrementTotalCount();
    Task<IEnumerable<GetCartItemDto>> GetAllCartItems(int userId);
    void IncrementTotalCount();
    Task RaiseEvent(AddCartItemDto request);
    Task SetTotalItemCount(AddCartItemDto request);
}
