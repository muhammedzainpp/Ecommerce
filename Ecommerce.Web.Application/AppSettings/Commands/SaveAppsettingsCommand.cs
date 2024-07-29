using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Domain.Entities;
using Ecommerce.Web.Shared.Reponses;
using Microsoft.EntityFrameworkCore;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;

namespace Ecommerce.Web.Application.AppSettings.Commands;
public class SaveAppsettingsCommand : ICommand<int>
{
    public required string Country { get; set; }
    public required string CurrencyName { get; set; }
    public required string CurrencySymbol { get; set; }
}

public class SaveAppsettingsCommandHandler(IAppDbContext context) : ICommandHandler<SaveAppsettingsCommand, int>
{
    private readonly IAppDbContext _context = context;
    public async Task<Response<int>> Handle(SaveAppsettingsCommand request, CancellationToken cancellationToken)
    {
        return await TryHandleAsync(request);
    }
    private async Task<Response<int>> TryHandleAsync(SaveAppsettingsCommand request)
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

    private async Task<int> SaveAsync(SaveAppsettingsCommand request)
    {
        AppSetting appSetting = await _context.AppSettings.FirstAsync(x=>x.Id == 1);
        
          AppSetting.Update
           ( appSetting,request.Country, request.CurrencyName, request.CurrencySymbol);
        await _context.SaveChangesAsync();
        return appSetting.Id;
    }
}
