using Ecommerce.Web.Application.Categories.Extenions;
using Ecommerce.Web.Application.Common.Extension;
using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Application.Products.Dtos;
using Ecommerce.Web.Shared.Reponses;
using Microsoft.EntityFrameworkCore;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;


namespace Ecommerce.Web.Application.Products.Queries;
public class GetProductsQuery : IQuery<IEnumerable<ProductDto>>
{

}

public class GetProductsQueryHandler(IAppDbContext context) : IQueryHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IAppDbContext _context = context;

    public async Task<Response<IEnumerable<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await TryHandleAsync();
    }

    private async Task<Response<IEnumerable<ProductDto>>> TryHandleAsync()
    {
        Response<IEnumerable<ProductDto>> response;
        try
        {
            var products = await GetProductsAsync();
            response = OnSuccess<IEnumerable<ProductDto>>(products);
        }
        catch (Exception ex)
        {
            response = OnError<IEnumerable<ProductDto>>(ex);
        }
        return response;
    }

    private  async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        return await  _context
               .Products
               .Include(x=>x.Category)
               .Select(x=>new ProductDto
        {
                   Id = x.Id,
                   Name = x.Name,
                   Description = x.Description,
                   Category = x.Category.ToCategoryDto(),
                   Cost = x.Cost.ConvertToMoneyDto(),
                   ImageUrl = x.ImageUrl,
                   CategoryId = x.CategoryId,
                   Quantity = x.Quantity
               }).ToListAsync();
    }
}


