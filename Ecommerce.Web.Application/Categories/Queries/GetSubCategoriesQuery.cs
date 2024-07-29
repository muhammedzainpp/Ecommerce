using Ecommerce.Web.Application.Categories.Dtos;
using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Shared.Reponses;
using Microsoft.EntityFrameworkCore;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;

namespace Ecommerce.Web.Application.Categories.Queries;
public class GetSubCategoriesQuery : IQuery<IEnumerable<CategoryDto>>
{
    public int ParentCategoryId { get; set; }
}
public class GetSubCategoriesQueryHandler(IAppDbContext context) : IQueryHandler<GetSubCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly IAppDbContext _context = context;

    public async Task<Response<IEnumerable<CategoryDto>>> Handle(GetSubCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await TryHandleAsync(request);
    }

    private async Task<Response<IEnumerable<CategoryDto>>> TryHandleAsync(GetSubCategoriesQuery request)
    {
        Response<IEnumerable<CategoryDto>> response;
        try
        {
            var products = await GetSubCategoriesAsync(request);
            response = OnSuccess<IEnumerable<CategoryDto>>(products);
        }
        catch (Exception ex)
        {
            response = OnError<IEnumerable<CategoryDto>>(ex);
        }
        return response;
    }

    private async Task<IEnumerable<CategoryDto>> GetSubCategoriesAsync(GetSubCategoriesQuery request)
    {
        return await  _context
                     .Categories
                     .Where(x => x.ParentCategoryId != null)
                     .Where(x=>x.ParentCategoryId == request.ParentCategoryId)
                     .Select(x => new CategoryDto
                     {
                         Id = x.Id,
                         Name = x.Name,
                         ParentCategoryId = x.ParentCategoryId,
                         ImageUrl = x.ImageUrl
                     })
                     .ToListAsync();
    }
}

