using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Application.Products.Dtos;
using Ecommerce.Web.Shared.Reponses;
using Microsoft.EntityFrameworkCore;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;


namespace Ecommerce.Web.Application.Products.Queries;
public class GetProductQuery : IQuery<ProductDto>
{
    public int Id { get; set; }
}

public class GetProductQueryHandler(IAppDbContext context) : IQueryHandler<GetProductQuery, ProductDto>
{
    private readonly IAppDbContext _context = context;

    public async Task<Response<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        return await TryHandleAsync(request);
    }

    private async Task<Response<ProductDto>> TryHandleAsync(GetProductQuery request)
    {
        Response<ProductDto> response;
        try
        {
            var product = await GetProductAsync(request);
            response = OnSuccess<ProductDto>(product);
        }
        catch (Exception ex)
        {
            response = OnError<ProductDto>(ex);
        }
        return response;
    }

    private async Task<ProductDto> GetProductAsync(GetProductQuery request)
    {
        return await _context.Products.Where(x => x.Id == request.Id)
            .Select(x => new ProductDto()
            {
                Id = x.Id,
                Title=x.Title,
                Description=x.Description,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                CategoryId = x.CategoryId
            })
            .FirstOrDefaultAsync() ?? throw new Exception("product not found");

    }
}

