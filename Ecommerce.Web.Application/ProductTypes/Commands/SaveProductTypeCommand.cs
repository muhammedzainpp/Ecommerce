using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Domain.Entities;
using Ecommerce.Web.Shared.Reponses;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;


namespace Ecommerce.Web.Application.ProductTypes.Commands;
public class SaveProductTypeCommand : ICommand<int>
{
    public required string Name { get; set; }
}

public class SaveProductTypeCommandHandler(IAppDbContext context) : ICommandHandler<SaveProductTypeCommand, int>
{
    private readonly IAppDbContext _context = context;

    public async Task<Response<int>> Handle(SaveProductTypeCommand request, CancellationToken cancellationToken)
    {
        return await TryHandleAsync(request);

    }

    private async Task<Response<int>> TryHandleAsync(SaveProductTypeCommand request)
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

    private async Task<int> SaveAsync(SaveProductTypeCommand request)
    {
        ProductType productType;
        productType = ProductType.Create(request.Name);

        await _context.ProductTypes.AddAsync(productType);

        await _context.SaveChangesAsync();
        return productType.Id;
    }
}
