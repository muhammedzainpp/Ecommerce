using Ecommerce.Web.Client.Services.Categories;
using Microsoft.AspNetCore.Components;

namespace Ecommerce.Web.Client.Shared.Common;

public partial class Home
{
    [Inject]
    public ICategoryService Service { get; set; } = default!;
    [Inject]
    public NavigationManager navigationManager { get; set; } = default!;
    public void GetSubCategories(int categoryId)
    {
        navigationManager.NavigateTo($"subcategories/{categoryId}");
    }
    protected override async Task OnInitializedAsync()
    {
        await Service.GetCategories();
    }
}
