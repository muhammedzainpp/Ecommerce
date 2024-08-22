using Ecommerce.Web.Client.Helpers;
using Ecommerce.Web.Client.Services;
using Ecommerce.Web.Client.Services.Carts;
using Ecommerce.Web.Client.Services.Carts.Dtos;
using Ecommerce.Web.Client.Services.Products;
using Ecommerce.Web.Client.Services.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Ecommerce.Web.Client.Shared.Products;

public partial class ProductsByCategory
{
    [Parameter]
    public int CategoryId { get; set; }
    [Inject]
    public required AppState AppState { get; set; }
    [Inject]
    public required ICartServices CartServices { get; set; }
    [Inject]
    public required IUserService UserService { get; set; }
    public int UserId { get; set; }
    public IEnumerable<GetCartItemDto> Products { get; set; } = new List<GetCartItemDto>();


    public int Id { get; set; }
    [Inject]
    public required IProductService Service { get; set; }

    [Inject]
    public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var userId = AppState.UserId;
        if (userId is not null) UserId = userId.Value;
        Products = await Service.GetProductsByCategory(CategoryId, UserId);
    }


    public async Task incrementcount(GetCartItemDto cartItem)
    {
        cartItem.Quantity++;
        CartServices.IncrementTotalCount();
        var product = cartItem.Product;
        var money = product.Cost.ToExactMoney(cartItem.Quantity);
        var cartitemDto = new AddCartItemDto
        {
            ProductId = product.Id,
            UserId = UserId,
            TotalPrice = money,
            Quantity = cartItem.Quantity,
        };
        await CartServices.SetTotalItemCount(cartitemDto);
    }

    public async Task decrementcount(GetCartItemDto cartItem )
    {
        cartItem.Quantity--;
        CartServices.DecrementTotalCount();
        var product = cartItem.Product;
        var money = product.Cost.ToExactMoney(cartItem.Quantity);
        var cartitemDto = new AddCartItemDto
        {
            ProductId = product.Id,
            UserId = UserId,
            TotalPrice  = money,
            Quantity = cartItem.Quantity
            
        };
        await CartServices.SetTotalItemCount(cartitemDto);
    }
}
