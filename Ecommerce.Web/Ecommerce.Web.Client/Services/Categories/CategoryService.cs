using Ecommerce.Web.Client.Services.Categories.Dtos;

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

}
