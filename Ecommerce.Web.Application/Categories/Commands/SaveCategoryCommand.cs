using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Domain.Entities;
using Ecommerce.Web.Shared.Reponses;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;



namespace Ecommerce.Web.Application.Categories.Commands;
public class SaveCategoryCommand : ICommand<int> 
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int? ParentCategoryId { get; set; }
    public  string? ImageUrl { get; set; }
}

public class SaveCategoryCommandHandler(IAppDbContext context) : ICommandHandler<SaveCategoryCommand, int>
{
    private readonly IAppDbContext _context = context;

    public async Task<Response<int>> Handle(SaveCategoryCommand request, CancellationToken cancellationToken)
    {
        return await TryHandleAsync(request);
    }
    private async Task<Response<int>> TryHandleAsync(SaveCategoryCommand request)
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
    private async Task<int> SaveAsync(SaveCategoryCommand request)
    {

        Category category;
     
        category =request.ParentCategoryId is  null 
                  ? Category.Create(null,request.Name,request.ImageUrl)
                  :Category.Create(request.ParentCategoryId,request.Name,request.ImageUrl);


        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category.Id;
    }
}

