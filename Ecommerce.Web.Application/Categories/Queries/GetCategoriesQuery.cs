using Ecommerce.Web.Application.Categories.Dtos;
using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Shared.Reponses;
using Microsoft.EntityFrameworkCore;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;

namespace Ecommerce.Web.Application.Categories.Queries;
public class GetCategoriesQuery : IQuery<IEnumerable<CategoryDto>>
{

}

public class GetCategoriesQueryHandler(IAppDbContext context) : IQueryHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly IAppDbContext _context = context;

    public async Task<Response<IEnumerable<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await TryHandleAsync();
    }

    private async Task<Response<IEnumerable<CategoryDto>>> TryHandleAsync()
    {
        Response<IEnumerable<CategoryDto>> response;
        try
        {
            var products = await GetCategoriesAsync();
            response = OnSuccess<IEnumerable<CategoryDto>>(products);
        }
        catch (Exception ex)
        {
            response = OnError<IEnumerable<CategoryDto>>(ex);
        }
        return response;
    }

    private async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        return await _context
                     .Categories
                     .Select(x => new CategoryDto
                      {
                        Id = x.Id,
                        Name = x.Name,
                        Url = x.Url,
                      })
                     .ToListAsync();
    }
}
