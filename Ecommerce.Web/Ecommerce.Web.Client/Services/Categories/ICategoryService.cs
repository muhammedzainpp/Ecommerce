using Ecommerce.Web.Client.Services.Categories.Dtos;
using Ecommerce.Web.Shared.Reponses;

namespace Ecommerce.Web.Client.Services.Categories;

public interface ICategoryService
{
    Task<Response<IEnumerable<CategoryDto>>> GetCategories();
    Task<IEnumerable<CategoryDto>> GetSubCategories(int parentCategoryId);
    Task<Response<int>> SaveMainCategory(CategoryDto request);
    
}
