using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Domain.Entities;
using Ecommerce.Web.Domain.Entities.Base;
using Ecommerce.Web.Domain.ValueObjects;
using Ecommerce.Web.Shared.Common.Enums;
using Ecommerce.Web.Shared.Reponses;
using Microsoft.EntityFrameworkCore;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;


namespace Ecommerce.Web.Application.Carts.Commands;
public class CreateCartCommand : ICommand<int>
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public CartActitvity Activity { get; set; }
}

public class CreateCartCommandHandler : ICommandHandler<CreateCartCommand, int>
{
    private readonly IAppDbContext _context;

    public CreateCartCommandHandler(IAppDbContext appDbContext)
    {
        _context = appDbContext;
    }
    public async Task<Response<int>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        return await TryHandleAsync(request);
    }

    private async Task<Response<int>> TryHandleAsync(CreateCartCommand request)
    {
        Response<int> response;
        try
        {
            var id = await SaveAsync(request);
            response = OnSuccess<int>(id);
        }
        catch (Exception ex)
        {
            response = OnError<int>(ex);
        }
        return response;
    }

    private async Task<int> SaveAsync(CreateCartCommand request)
    {

        Cart? cart;
        cart = await IsCartExist(request.UserId);
        if (cart is not null)
            await UpdateAsync(cart, request);
        else
        {
            cart = Cart.Create(request.UserId);
            await _context.Carts.AddAsync(cart);
            await CreateNewCartItem(request);
        }

        await _context.SaveChangesAsync();
        return cart.Id;
    }


    public async Task<Cart?> IsCartExist(int userId)
    {
        var cart = await _context
            .Carts
            .Include(x => x.CartItems)
            .FirstOrDefaultAsync(x => x.Id == userId);
        return cart;
    }
    private async Task<int> UpdateAsync(Cart cart, CreateCartCommand request)
    {
        CartItem? cartItem = await _context
                                   .CartItems
                                   .FirstOrDefaultAsync(x => x.CartId == request.UserId && x.ProductId == request.ProductId);

        if (cartItem is not null)
            await UpdateCartItem(cart, request);
        else
            await CreateNewCartItem(request);

        return cart.Id;
    }

    public async Task UpdateCartItem(Cart cart, CreateCartCommand request)
    {
        var product = await _context
            .Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId) ??
                throw new Exception("Product Not Found");

        string currencyName = product.Cost.Currency.Name;
        string symbol = product.Cost.Currency.Symbol;
        var cartItem = await _context.CartItems.FirstOrDefaultAsync(x => x.ProductId == request.ProductId && x.CartId == request.UserId) ?? throw new Exception("Cart Item Not Found");
        if (request.Activity == CartActitvity.Increment)
            cartItem.Quantity++;
        else
            cartItem.Quantity--;

        decimal amount = product.Cost.Amount * cartItem.Quantity;
        cartItem.TotalPrice = Money.Create(currencyName, symbol, amount);
    }

    public async Task CreateNewCartItem(CreateCartCommand request)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId) ?? throw new Exception("Product Not Found");
        string currencyName = product.Cost.Currency.Name;
        string symbol = product.Cost.Currency.Symbol;
        decimal amount = product.Cost.Amount;
        int quantity = 1;

        CartItem cartItem = new CartItem()
        {
            CartId = request.UserId,
            ProductId = request.ProductId,
            Quantity = quantity,
            TotalPrice = Money.Create(currencyName, symbol, amount)
        };

        await _context.CartItems.AddAsync(cartItem);
    }
}
