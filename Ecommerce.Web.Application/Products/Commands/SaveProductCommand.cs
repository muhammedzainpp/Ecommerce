using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Domain.Entities;
using Ecommerce.Web.Domain.ValueObjects;
using Ecommerce.Web.Shared.Reponses;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;

namespace Ecommerce.Web.Application.Products.Commands;
public class SaveProductCommand : ICommand<int>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public required Money Price { get; set; }
    public required int CategoryId { get; set; }
}

public class SaveProductCommandHandler(IAppDbContext context) : ICommandHandler<SaveProductCommand, int>
{
    private readonly IAppDbContext _context = context;

    public async Task<Response<int>> Handle(SaveProductCommand request, CancellationToken cancellationToken)
    {
        return await TryHandleAsync(request);
    }

    private async Task<Response<int>> TryHandleAsync(SaveProductCommand request)
    {
        Response<int> response;
        try
        {
            var id = await SaveAsync(request);
            response = OnSuccess<int>(id);
        }
        catch (Exception ex)
        {
           response =  OnError<int>(ex);
        }
        return response;
    }

    private async Task<int> SaveAsync(SaveProductCommand request)
    {
        Product product;
            product = Product.Create
               (request.Title, request.Description, request.ImageUrl,request.Price,request.CategoryId);

            await _context.Products.AddAsync(product);

        await _context.SaveChangesAsync();
        return product.Id;
    }
}
            
