using Ecommerce.Web.Client.Services.Carts;
using Ecommerce.Web.Client.Services.Carts.Dtos;

namespace Ecommerce.Web.Client.EventSubscribers;

public class CartEventSubscriber

{
    private readonly ICartServices _cartServices;

    public CartEventSubscriber(ICartServices cartServices)
    {
        _cartServices = cartServices;
    }
    public async Task AddToCart(AddCartItemDto request)
    {
        await _cartServices.AddToCart(request);
    }
}
