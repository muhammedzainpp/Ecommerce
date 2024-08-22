using Ecommerce.Web.Application.Common.Extension;
using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Domain.Entities;
using Ecommerce.Web.Domain.Entities.Base;
using Ecommerce.Web.Shared.Common.  Dtos;
using Ecommerce.Web.Shared.Reponses;
using Microsoft.EntityFrameworkCore;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;


namespace Ecommerce.Web.Application.Carts.Commands;
public class CreateCartCommand : ICommand<int>
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public required MoneyDto TotalPrice { get; set; }
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
            await UpdateAsync(cart,request);
        else
        {
            cart = Cart.Create(request.UserId);
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            await CreateNewCartItem(cart,request);
        }

        await _context.SaveChangesAsync();
        return cart.Id;
    }

    public async Task<Cart?> IsCartExist(int userId)
    {
        var cart = await _context
            .Carts
            .FirstOrDefaultAsync(x => x.UserId == userId);
        return cart;
    }
    private async Task UpdateAsync(Cart cart,CreateCartCommand request)
    {
        CartItem? cartItem = await _context
                                   .CartItems
                                   .FirstOrDefaultAsync(x => x.CartId == cart.Id && x.ProductId == request.ProductId);

        if (cartItem is not null)
            UpdateCartItem(request, cartItem);
        else
            await CreateNewCartItem(cart,request);

    }

    public async void UpdateCartItem(CreateCartCommand request, CartItem cartItem)
    {
        //var product = await _context
        //    .Products
        //    .FirstOrDefaultAsync(x => x.Id == request.ProductId) ??
        //        throw new Exception("Product Not Found");

        //string currencyName = product.Cost.Currency.Name;
        //string symbol = product.Cost.Currency.Symbol;
        //if(request.Actitvity == CartActivity.)
        //cartItem.Quantity = request.Quantity;


        if (request.Quantity is 0 || request.Quantity<0)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
        cartItem.Quantity = request.Quantity;
        cartItem.TotalPrice = request.TotalPrice.ConvertToMoney();

        //decimal amount = product.Cost.Amount * cartItem.Quantity;
        //cartItem.TotalPrice = Money.Create(currencyName, symbol, amount);
    }

    public async Task CreateNewCartItem(Cart cart,CreateCartCommand request)
    {
        //var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId) ?? throw new Exception("Product Not Found");
        //string currencyName = product.Cost.Currency.Name;
        //string symbol = product.Cost.Currency.Symbol;
        //decimal amount = product.Cost.Amount;
        //int quantity = 1;

        CartItem cartItem = new CartItem()
        {
            CartId = cart.Id,
            ProductId = request.ProductId,
            Quantity = 1,
            TotalPrice = request.TotalPrice.ConvertToMoney(),
        };

        await _context.CartItems.AddAsync(cartItem);
    }
}
