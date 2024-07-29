using Ecommerce.Web.Client.Services.AppSettings.Dtos;
using Ecommerce.Web.Shared.Reponses;

namespace Ecommerce.Web.Client.Services.AppSettings
{
	public class AppSettingsService(IApiService apiService) : IAppSettingsService
	{
		private const string _baseUrl = "api/appsettings";

		private readonly IApiService _apiservice = apiService;

		public async Task<Response<int>> SaveAppsettings(AppSettingsDto request)
		{
			var response = await _apiservice.Post<AppSettingsDto>(_baseUrl, request);
			return response;
		}

		public async Task<AppSettingsDto> GetAppSetings()
		{
			var response = await _apiservice.Get<AppSettingsDto>($"{_baseUrl}");
			if (response != null && response.Result != null)
				return response.Result;
			else
				throw new Exception();
		}
	}
}
