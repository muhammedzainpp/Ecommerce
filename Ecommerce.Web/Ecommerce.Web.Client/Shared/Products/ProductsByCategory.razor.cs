
using Ecommerce.Web.Client.EventSubscribers;
using Ecommerce.Web.Client.Services.Carts;
using Ecommerce.Web.Client.Services.Carts.Dtos;
using Ecommerce.Web.Client.Services.Products;
using Ecommerce.Web.Client.Services.Products.Dtos;
using Ecommerce.Web.Client.Services.Users;
using Ecommerce.Web.Shared.Common.Dtos;
using Ecommerce.Web.Shared.Common.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Ecommerce.Web.Client.Shared.Products;

public partial class ProductsByCategory
{
    [Parameter]
    public int CategoryId { get; set; }


    [Inject]
    public required ICartServices CartServices  { get; set; }
    [Inject]
    public required IUserService UserService { get; set; }

    public int UserId { get; set; }
    public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();

    [Inject]
    public required CartEventSubscriber CartEventService { get; set; }


    [Inject]
    public required IProductService Service { get; set; }

    [Inject]
    public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    protected override async Task OnInitializedAsync()
    {

        CartServices.OnButtonClicked += CartEventService.AddToCart;
        Products = await Service.GetProductsByCategory(CategoryId);
        int? userId =  await GetCurrentUserId();
        if (userId is not null) UserId = userId.Value;
    }

    private async Task<int?> GetCurrentUserId()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        if(authState is not null && authState.User.Identity is not null)
        {
            var userId = authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ;
            var loggedInUser = await UserService.GetUserAsync(userId);
            var currentUserId = loggedInUser?.Id;

            return currentUserId;
        }

        return null;
       
    }

    public async Task incrementcount(int productId)
    {
        var cartitemDto = new CartItemDto
        { 
            ProductId = productId,
            UserId = UserId,
            Activity = CartActitvity.Increment,
            TotalPrice = new MoneyDto()
        };
        await CartServices.SetTotalItemCount(cartitemDto);
    }

    public void Dispose()
    {
        CartServices.OnButtonClicked -= CartEventService.AddToCart;
    }



}
