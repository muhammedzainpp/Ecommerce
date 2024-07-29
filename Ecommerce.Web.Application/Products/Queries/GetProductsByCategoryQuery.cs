using Ecommerce.Web.Application.Categories.Extenions;
using Ecommerce.Web.Application.Common.Extension;
using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Application.Products.Dtos;
using Ecommerce.Web.Shared.Reponses;
using Microsoft.EntityFrameworkCore;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;


namespace Ecommerce.Web.Application.Products.Queries;

public class GetProductsByCategoryQuery : IQuery<IEnumerable<ProductDto>>
{
    public int CategoryId { get; set; }
}

public class GetProductsByCategoryQueryHandler(IAppDbContext context) : IQueryHandler<GetProductsByCategoryQuery, IEnumerable<ProductDto>>
{
    private readonly IAppDbContext _context = context;

    public async Task<Response<IEnumerable<ProductDto>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        return await TryHandleAsync(request);
    }

    private async Task<Response<IEnumerable<ProductDto>>> TryHandleAsync(GetProductsByCategoryQuery request)
    {
        Response<IEnumerable<ProductDto>> response;
        try
        {
            var products = await GetProductsAsync(request);
            response = OnSuccess<IEnumerable<ProductDto>>(products);
        }
        catch (Exception ex)
        {
            response = OnError<IEnumerable<ProductDto>>(ex);
        }
        return response;
    }

    private async Task<IEnumerable<ProductDto>> GetProductsAsync(GetProductsByCategoryQuery request)
    {

        return await _context
               .Products
               .Where(x=>x.CategoryId == request.CategoryId)
               .Include(x=>x.Category)
               .Select(x => new ProductDto
               {
                   Id = x.Id,
                   Name = x.Name,
                   Description = x.Description,
                   Category = x.Category.ToCategoryDto(),
                   ImageUrl = x.ImageUrl,
                   Cost = x.Cost.ConvertToMoneyDto(),
                   CategoryId= x.CategoryId,
                   Quantity = x.Quantity,
               }) .ToListAsync();
    }
}
