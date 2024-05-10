using Ecommerce.Web.Client.Services.Categories;
using Ecommerce.Web.Client.Services.Categories.Dtos;
using Microsoft.AspNetCore.Components;

namespace Ecommerce.Web.Client.Shared.Admin.Categories;

public partial class SaveSubCategory
{
    [Inject]
    public ICategoryService CategoryService { get; set; } = default!;

    [SupplyParameterFromForm]
    public CategoryDto SubCategory { get; set; } = new CategoryDto();

    protected override async Task OnInitializedAsync()
    {
       await CategoryService.GetCategories();
    }

    public async Task SaveSubCategoryAsync()
    {
        await CategoryService.SaveMainCategory(SubCategory);
    }



}
