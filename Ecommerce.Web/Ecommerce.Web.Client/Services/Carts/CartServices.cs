using Ecommerce.Web.Client.Services.Carts.Dtos;
using Ecommerce.Web.Shared.Reponses;

namespace Ecommerce.Web.Client.Services.Carts;

public class CartServices: ICartServices
{
    private const string _baseUrl = "api/carts";
    private readonly IApiService _apiService;
    public CartServices(IApiService apiService)
    {
        _apiService = apiService;
    }
    public Func<AddCartItemDto, Task>? OnButtonClicked { get; set; }

    public int TotalItemCount { get; private set; }
    public async Task RaiseEvent(AddCartItemDto request)
    {
        if (OnButtonClicked != null)
        {
            await OnButtonClicked.Invoke(request);
        }
    }

    public async Task SetTotalItemCount(AddCartItemDto request)
    {
        TotalItemCount++;
        await RaiseEvent(request);
    }
    public async Task<Response<int>> AddToCart(AddCartItemDto request)
    {
        var response = await _apiService.Post<AddCartItemDto>(_baseUrl, request);
        return response;
    }


    public async Task<IEnumerable<GetCartItemDto>> GetAllCartItems(int userId)
    {

        var resposne = await _apiService.GetById<IEnumerable<GetCartItemDto>>(_baseUrl, userId);
        if (resposne is null || resposne.Result is null) return null;
        else return resposne.Result;
    }
}
