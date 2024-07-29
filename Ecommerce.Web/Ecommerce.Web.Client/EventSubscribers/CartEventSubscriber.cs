using Ecommerce.Web.Client.Services.Carts;
using Ecommerce.Web.Client.Services.Carts.Dtos;

namespace Ecommerce.Web.Client.EventSubscribers;

public class CartEventSubscriber(ICartServices cartServices)
{
    public async Task AddToCart(CartItemDto request)
    {
        await cartServices.AddToCart(request);
    }
}
