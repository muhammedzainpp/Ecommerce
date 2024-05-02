using Ecommerce.Web.Application.Products.Queries;
using Ecommerce.Web.Client.Services.Categories;
using Microsoft.AspNetCore.Components;

namespace Ecommerce.Web.Components.Pages;

public partial class Home
{
    [Inject]
    public ICategoryService CategoryService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await CategoryService.GetCategories();
    }
    [Inject]
    public NavigationManager navigationManager { get; set; } = default!;
    public void GetProductsByCategory(int categoryId)
    {
        navigationManager.NavigateTo($"products/{categoryId}");
    }
}
