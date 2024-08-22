using Ecommerce.Web.Client.Services;
using Ecommerce.Web.Client.Services.Carts;
using Ecommerce.Web.Client.Services.Carts.Dtos;
using Ecommerce.Web.Client.Services.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Ecommerce.Web.Client.Shared.Carts;

public partial class Cart
{
    [Inject]
    public required AppState AppState { get; set; }
    public int UserId { get; set; }
    [Inject]
    public required IUserService UserService { get; set; }
    [Inject]
    public required ICartServices CartService { get; set; }
    public IEnumerable<GetCartItemDto> CartItems { get; set; } = new List<GetCartItemDto>();
    protected override async Task OnInitializedAsync()
    {
        int? userId = AppState.UserId;
        if (userId is not null) UserId = userId.Value;
        CartItems = await CartService.GetAllCartItems(UserId);
    }

}
