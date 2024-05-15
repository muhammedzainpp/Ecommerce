using Ecommerce.Web.Client.Services.Categories.Dtos;
using Ecommerce.Web.Shared.Reponses;

namespace Ecommerce.Web.Client.Services.Categories;
public class CategoryService(IApiService apiService) : ICategoryService
{
    private const string _baseUrl = "api/categories";

    private readonly IApiService _apiservice = apiService;

    public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    public async Task GetCategories()
    {
        var response = await _apiservice.Get<IEnumerable<CategoryDto>>(_baseUrl);
        if (response != null && response.Result != null)
                Categories = response.Result;
    }
    public async Task<IEnumerable<CategoryDto>> GetSubCategories(int parentCategoryId)
    {
        var response = await _apiservice.GetById<IEnumerable<CategoryDto>>(_baseUrl+"/GetSubCategories",parentCategoryId);
        if (response != null && response.Result != null)
            return response.Result;
        else
            throw new Exception();      
    }
    public async Task<Response<int>> SaveMainCategory(CategoryDto request)
    {
        var response = await _apiservice.Post<CategoryDto>(_baseUrl, request);
        return response;
    }
}
