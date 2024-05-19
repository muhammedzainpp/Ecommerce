using Blazored.Toast.Services;
using Ecommerce.Web.Client.Services.Categories;
using Ecommerce.Web.Client.Services.Categories.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Ecommerce.Web.Client.Shared.Admin.Categories;

public partial class SaveCategory
{
    [Inject]
    public required IToastService ToastService { get; set; } 

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
            ToastService.ShowError(String.Join(",", response.Errors));
    }

}
