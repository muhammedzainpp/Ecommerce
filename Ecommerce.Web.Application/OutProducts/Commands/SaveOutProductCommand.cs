using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Domain.Entities;
using Ecommerce.Web.Domain.ValueObjects;
using Ecommerce.Web.Shared.Reponses;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;


namespace Ecommerce.Web.Application.OutProducts.Commands;
public class SaveOutProductCommand : ICommand<int>
{
    public  required int ProductId { get; set; }
    public required Money Price { get; set; }
    public required string ImageUrl { get; set; }
}
public class SaveOutProductCommandHandler(IAppDbContext _context) : ICommandHandler<SaveOutProductCommand, int>
{
    public async Task<Response<int>> Handle(SaveOutProductCommand request, CancellationToken cancellationToken)
    {
        return await TryHandleAsync(request);
    }
    private async Task<Response<int>> TryHandleAsync(SaveOutProductCommand request)
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

    private async Task<int> SaveAsync(SaveOutProductCommand request)
    {
        Item outProduct;
        outProduct = Item.Create
           (request.ProductId, request.Price,request.ImageUrl);
        await _context.Items.AddAsync(outProduct);
        await _context.SaveChangesAsync();
        return outProduct.Id;
    }
}

