using Ecommerce.Web.Client.Services.Categories;
using Ecommerce.Web.Client.Services.Categories.Dtos;
using Microsoft.AspNetCore.Components;

namespace Ecommerce.Web.Client.Shared.Admin.Categories;

public partial class SaveCategory
{
    [Inject]
    public ICategoryService CategoryService { get; set; } = default!;
    [SupplyParameterFromForm]
    public CategoryDto Category { get; set; } = new CategoryDto();
    public async Task SaveMainCategory() 
    {
            await CategoryService.SaveMainCategory(Category);
    }

}
