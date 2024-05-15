using Ecommerce.Web.Client.Services.Categories;
using Ecommerce.Web.Client.Services.Categories.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Ecommerce.Web.Client.Shared.Admin.Categories;

public partial class SaveCategory
{
    [Inject] 
    public IJSRuntime JS { get; set; } = default!;
    [Inject]
    public ICategoryService CategoryService { get; set; } = default!;
    [SupplyParameterFromForm]
    public CategoryDto Category { get; set; } = new CategoryDto();
    public async Task SaveMainCategory() 
    {
         var response = await CategoryService.SaveMainCategory(Category);
        if (!response.IsSuccess)
             Console.WriteLine();

    }

}
