using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Application.OutProducts.Dtos;
using Ecommerce.Web.Shared.Reponses;
using Microsoft.EntityFrameworkCore;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;


namespace Ecommerce.Web.Application.OutProducts.Queries;
public class GetOutProductsByBaseProductCommand : ICommand<IEnumerable<OutProductDto>>
{
    public int ProductId { get; set; }
}
public class GetOutProductsByBaseProductCommandHandler(IAppDbContext _context) : ICommandHandler<GetOutProductsByBaseProductCommand,IEnumerable<OutProductDto>>
{
    public int ProductId { get; set; }

    public async Task<Response<IEnumerable<OutProductDto>>> Handle(GetOutProductsByBaseProductCommand request, CancellationToken cancellationToken)
    {
        return await TryHandleAsync(request);
    }
    private async Task<Response<IEnumerable<OutProductDto>>> TryHandleAsync(GetOutProductsByBaseProductCommand request)
    {
        Response<IEnumerable<OutProductDto>> response;
        try
        {
            var products = await GetProductsAsync(request);
            response = OnSuccess<IEnumerable<OutProductDto>>(products);
        }
        catch (Exception ex)
        {
            response = OnError<IEnumerable<OutProductDto>>(ex);
        }
        return response;
    }

    private async Task<IEnumerable<OutProductDto>> GetProductsAsync(GetOutProductsByBaseProductCommand request)
    {
        return await _context
               .OutProducts
               .Where(x => x.ProductId == request.ProductId)
               .Select(x => new OutProductDto
               {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Price = x.Price,
                    ImageUrl= x.ImageUrl
               }).ToListAsync();
    }
}

