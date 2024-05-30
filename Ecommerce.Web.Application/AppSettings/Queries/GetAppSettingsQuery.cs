using Ecommerce.Web.Application.Common.Interfaces.Mediatr;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Shared.Reponses;
using Microsoft.EntityFrameworkCore;
using static Ecommerce.Web.Application.Common.Helpers.ResponseHelpers;


namespace Ecommerce.Web.Application.AppSettings.Queries;
public class GetAppSettingsQuery : IQuery<AppSettingDto>
{

}

public class GetAppSettingsQueryHandler(IAppDbContext context) : IQueryHandler<GetAppSettingsQuery,AppSettingDto>
{
    private readonly IAppDbContext _context = context;

    public async Task<Response<AppSettingDto>> Handle(GetAppSettingsQuery request, CancellationToken cancellationToken)
    {
        return await TryHandleAsync();
    }

    private async Task<Response<AppSettingDto>> TryHandleAsync()
    {
        Response<AppSettingDto> response;
        try
        {
            var products = await GetCategoriesAsync();
            response = OnSuccess<AppSettingDto>(products);
        }
        catch (Exception ex)
        {
            response = OnError<AppSettingDto>(ex);
        }
        return response;
    }

    private async Task<AppSettingDto> GetCategoriesAsync()
    {
        return await _context
                     .AppSettings
                     .Select(x => new AppSettingDto
                     {
                         Id = x.Id,
                         Country = x.Country,
                         CurrencyName = x.CurrencyName,
                         CurrencySymbol = x.CurrencySymbol
                     })
                     .FirstAsync() ?? throw new Exception();
    }
}

public class AppSettingDto
{
    public int Id { get; set; }
    public required string Country { get; set; }
    public required string CurrencyName { get; set; }
    public required string CurrencySymbol { get; set; }
}