using Ecommerce.Web.Client.Services.AppSettings.Dtos;
using Ecommerce.Web.Shared.Reponses;

namespace Ecommerce.Web.Client.Services.AppSettings;

public interface IAppSettingsService
{
    Task<AppSettingsDto> GetAppSetings();
    Task<Response<int>> SaveAppsettings(AppSettingsDto request);
}
