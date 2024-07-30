using Ecommerce.Web.Client.Services.Carts;
using Ecommerce.Web.Client.Services.Carts.Dtos;
using Ecommerce.Web.Client.Services.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Ecommerce.Web.Client.Shared.Carts;

public partial class Cart
{
    public int UserId { get; set; }
    [Inject]
    public required IUserService UserService { get; set; }
    [Inject]
    public required ICartServices CartService { get; set; }
    public IEnumerable<GetCartItemDto> CartItems { get; set; } = new List<GetCartItemDto>();

    [Inject]
    public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    protected override async Task OnInitializedAsync()
    {
        int? userId = await GetCurrentUserId();
        if (userId is not null) UserId = userId.Value;
        CartItems = await CartService.GetAllCartItems(UserId);

    }


    private async Task<int?> GetCurrentUserId()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        if (authState is not null && authState.User.Identity is not null)
        {
            var userId = authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var loggedInUser = await UserService.GetUserAsync(userId);
            var currentUserId = loggedInUser?.Id;

            return currentUserId;
        }

        return null;

    }
}
