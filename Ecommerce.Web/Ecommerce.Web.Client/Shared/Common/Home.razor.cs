using Ecommerce.Web.Client.Services.Categories;
using Ecommerce.Web.Client.Services.Categories.Dtos;
using Microsoft.AspNetCore.Components;

namespace Ecommerce.Web.Client.Shared.Common;

public partial class Home
{
   
    [Inject]
    public ICategoryService Service { get; set; } = default!;
    [Inject]
    public NavigationManager navigationManager { get; set; } = default!;

    public CategoryDto FirstCategory { get; set; } = new CategoryDto();
    public void GetSubCategories(int categoryId)
    {
        navigationManager.NavigateTo($"subcategories/{categoryId}");
    }


    public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();  
    protected override async Task OnInitializedAsync()
    {
        var categoriesRes = await Service.GetCategories();
        if (categoriesRes.IsSuccess && categoriesRes.Result is not null)
        {

            Categories = categoriesRes.Result;
            Categories.ToList().RemoveAt(0);
            FirstCategory = categoriesRes.Result.First();
            
        }
    }
}
