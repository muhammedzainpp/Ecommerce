using Ecommerce.Web.Application.Carts.Dtos;
using Ecommerce.Web.Application.Common.Extension;
using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Domain.Entities.Base;
using Ecommerce.Web.Shared.Reponses;
using Microsoft.EntityFrameworkCore;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;


namespace Ecommerce.Web.Application.Carts.Queries;
public class GetCartItemsByUser : IQuery<IEnumerable<CartItemDto>>
{
    public int UserId { get; set; }
}


public class GetCartItemsByUserHandler(IAppDbContext _context) : IQueryHandler<GetCartItemsByUser, IEnumerable<CartItemDto>>
{
    public async Task<Response<IEnumerable<CartItemDto>>> Handle(GetCartItemsByUser request, CancellationToken cancellationToken)
    {
        return await TryHandleAsync(request);
    }

    private async Task<Response<IEnumerable<CartItemDto>>> TryHandleAsync(GetCartItemsByUser request)
    {
        Response<IEnumerable<CartItemDto>> response;
        try
        {
            var cartItems = await GetCartItemsAsync(request);
            response = OnSuccess<IEnumerable<CartItemDto>>(cartItems);
        }
        catch (Exception ex)
        {
            response = OnError<IEnumerable<CartItemDto>>(ex);
        }
        return response;
    }

    private async Task<IEnumerable<CartItemDto>> GetCartItemsAsync(GetCartItemsByUser request)
    {
        Cart cart = _context
                    .Carts
                    .Include(x => x.CartItems)
                    .FirstOrDefault(x => x.Id == request.UserId) ?? throw new Exception("Cart Not Found");
         
         return cart.CartItems.Select(x => new CartItemDto
         {
             Id = x.Id,
             UserId = x.CartId,
             ProductId = x.ProductId,
             Quantity = x.Quantity,
             TotalPrice = x.TotalPrice.ConvertToMoneyDto()
         }); 
    }
}