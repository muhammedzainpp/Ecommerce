using Ecommerce.Web.Client.Services.Categories;
using Ecommerce.Web.Client.Services.Categories.Dtos;
using Ecommerce.Web.Client.Services.Products;
using Ecommerce.Web.Client.Services.Products.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Ecommerce.Web.Client.Shared.Categories;

public partial class SubCategoriesList
{
    [Parameter]
    public int ParentCategoryId { get; set; }

    [Inject]
    public required ICategoryService Service { get; set; }
    [Inject]
    public IProductService ProductService { get; set; } = default!;
    [Inject]
    public NavigationManager navigationManager { get; set; } = default!;
    [Inject]
    public IJSRuntime JS { get; set; } = default!;
    public IEnumerable<CategoryDto> SubCategories { get; set; } = new List<CategoryDto>();
    public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();

    protected override async Task OnParametersSetAsync()
    {
        SubCategories = await Service.GetSubCategories(ParentCategoryId);
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JS.InvokeVoidAsync("initBs5");
        }

    }
    public async Task ToggleModal1(int categoryId)
    {
        Products = await ProductService.GetProductsByCategory(categoryId);
        await JS.InvokeVoidAsync("toggleModal1");
    }
    public void GetOutProduct(int productId)
    {
        navigationManager.NavigateTo($"outproducts/{productId}");
    }


}
