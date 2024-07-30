using Ecommerce.Web.Application.Carts.Dtos;
using Ecommerce.Web.Application.Common.Extension;
using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Application.Products.Extensions;
using Ecommerce.Web.Domain.Entities.Base;
using Ecommerce.Web.Shared.Reponses;
using Microsoft.EntityFrameworkCore;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;


namespace Ecommerce.Web.Application.Carts.Queries;
public class GetCartItemsByUser : IQuery<IEnumerable<GetCartItemDto>>
{
    public int UserId { get; set; }
}


public class GetCartItemsByUserHandler(IAppDbContext _context) : IQueryHandler<GetCartItemsByUser, IEnumerable<GetCartItemDto>>
{
    public async Task<Response<IEnumerable<GetCartItemDto>>> Handle(GetCartItemsByUser request, CancellationToken cancellationToken)
    {
        return await TryHandleAsync(request);
    }

    private async Task<Response<IEnumerable<GetCartItemDto>>> TryHandleAsync(GetCartItemsByUser request)
    {
        Response<IEnumerable<GetCartItemDto>> response;
        try
        {
            var cartItems = await GetCartItemsAsync(request);
            response = OnSuccess<IEnumerable<GetCartItemDto>>(cartItems);
        }
        catch (Exception ex)
        {
            response = OnError<IEnumerable<GetCartItemDto>>(ex);
        }
        return response;
    }

    private async Task<IEnumerable<GetCartItemDto>> GetCartItemsAsync(GetCartItemsByUser request)
    {
        Cart cart = _context
                    .Carts
                    .Include(x => x.CartItems)
                    .ThenInclude(x=>x.Product)
                    .FirstOrDefault(x => x.Id == request.UserId) ?? throw new Exception("Cart Not Found");
         
         return cart.CartItems.Select(x => new GetCartItemDto
         {
             Id = x.Id,
             UserId = x.CartId,
             Product = x.Product.ToProductDto(),
             ProductId = x.ProductId,
             Quantity = x.Quantity,
             TotalPrice = x.TotalPrice.ConvertToMoneyDto()
         }); 
    }
}