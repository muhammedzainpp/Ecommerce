using Ecommerce.Web.Client.Services.Categories.Dtos;

namespace Ecommerce.Web.Client.Services.Categories;

public interface ICategoryService
{
    IEnumerable<CategoryDto> Categories { get; set; }

    Task GetCategories();
    Task<IEnumerable<CategoryDto>> GetSubCategories(int parentCategoryId);
    Task SaveMainCategory(CategoryDto request);
    
}
