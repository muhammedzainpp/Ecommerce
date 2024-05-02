using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Domain.Entities;
using Ecommerce.Web.Shared.Reponses;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;


namespace Ecommerce.Web.Application.ProductVariants.Commands;
public class SaveProductVariantCommand : ICommand<int>
{
    public required int ProductId { get; set; }
    public required int ProductTypeId { get; set; }

}

public class SaveProductVariantCommandHandler(IAppDbContext context) : ICommandHandler<SaveProductVariantCommand, int>
{
    private readonly IAppDbContext _context = context;

    public async Task<Response<int>> Handle(SaveProductVariantCommand request, CancellationToken cancellationToken)
    {
        return await TryHandleAsync(request);
    }

    private async Task<Response<int>> TryHandleAsync(SaveProductVariantCommand request)
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
    private async Task<int> SaveAsync(SaveProductVariantCommand request)
    {
        ProductVariant productVariant;
        productVariant = ProductVariant.Create(request.ProductId,request.ProductTypeId);

        await _context.ProductVariants.AddAsync(productVariant);

        await _context.SaveChangesAsync();
        return productVariant.Id;
    }
}


