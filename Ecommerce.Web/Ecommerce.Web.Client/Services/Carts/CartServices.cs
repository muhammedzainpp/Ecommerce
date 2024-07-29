using Ecommerce.Web.Client.Services.Carts.Dtos;
using Ecommerce.Web.Shared.Reponses;

namespace Ecommerce.Web.Client.Services.Carts;

public class CartServices(IApiService apiService) : ICartServices
{
    private const string _baseUrl = "api/carts";
    private readonly IApiService _apiService = apiService;

    public Func<CartItemDto, Task>? OnButtonClicked { get; set; }

    public int TotalItemCount { get; private set; }
    public async Task RaiseEvent(CartItemDto request)
    {
        if (OnButtonClicked != null)
        {
            await OnButtonClicked.Invoke(request);
        }
    }

    public async Task SetTotalItemCount(CartItemDto request)
    {
        TotalItemCount++;
        await RaiseEvent(request);
    }
    public async Task<Response<int>> AddToCart(CartItemDto request)
    {
        var response = await _apiService.Post<CartItemDto>(_baseUrl, request);
        return response;
    }


    public async Task<IEnumerable<CartItemDto>> GetAllCartItems(int userId)
    {

        var resposne = await _apiService.GetById<IEnumerable<CartItemDto>>(_baseUrl, userId);
        return resposne.Result;
    }
}
