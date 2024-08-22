using Ecommerce.Web.Application.Carts.Dtos;
using Ecommerce.Web.Application.Categories.Extenions;
using Ecommerce.Web.Application.Common.Extension;
using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Application.Products.Dtos;
using Ecommerce.Web.Application.Products.Extensions;
using Ecommerce.Web.Shared.Reponses;
using Microsoft.EntityFrameworkCore;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;


namespace Ecommerce.Web.Application.Products.Queries;

public class GetProductsByCategoryQuery : IQuery<IEnumerable<GetCartItemDto>>
{
    public int CategoryId { get; set; }
    public int UserId { get; set; }
}

public class GetProductsByCategoryQueryHandler(IAppDbContext context) : IQueryHandler<GetProductsByCategoryQuery, IEnumerable<GetCartItemDto>>
{
    private readonly IAppDbContext _context = context;

    public async Task<Response<IEnumerable<GetCartItemDto>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        return await TryHandleAsync(request);
    }

    private async Task<Response<IEnumerable<GetCartItemDto>>> TryHandleAsync(GetProductsByCategoryQuery request)
    {
        Response<IEnumerable<GetCartItemDto>> response;
        try
        {
            var products = await GetProductsAsync(request);
            response = OnSuccess<IEnumerable<GetCartItemDto>>(products);
        }
        catch (Exception ex)
        {
            response = OnError<IEnumerable<GetCartItemDto>>(ex);
        }
        return response;
    }

    private async Task<IEnumerable<GetCartItemDto>> GetProductsAsync(GetProductsByCategoryQuery request)
    {
        var cartItems = await _context
       .Carts
       .Include(x => x.CartItems)
       .Where(x => x.UserId == request.UserId)
       .SelectMany(x => x.CartItems)
       .ToListAsync();

        var cartItemQuantities = cartItems.ToDictionary(ci => ci.ProductId, ci => ci.Quantity);

        var products = await _context
                  .Products
                  .Where(x => x.CategoryId == request.CategoryId)
                  .Include(x => x.Category)
                  .ToListAsync();



        var productDtos = products.Select(product => new GetCartItemDto
        {
            ProductId = product.Id,
            Product = product.ToProductDto(),
            Quantity = cartItemQuantities.ContainsKey(product.Id) ? cartItemQuantities[product.Id] : 0
        }).ToList();

        return productDtos;
    }
}
