using Ecommerce.Web.Client.Services.Categories;
using Ecommerce.Web.Client.Services.Categories.Dtos;
using Ecommerce.Web.Client.Services.Products;
using Ecommerce.Web.Client.Services.Products.Dtos;
using Microsoft.AspNetCore.Components;

namespace Ecommerce.Web.Client.Shared.Admin.Products;

public partial class SaveProduct
{
    public int CategoryId { get; set; }

    [Inject]
    public ICategoryService CategoryService { get; set; } = default!;
    [Inject]
    public IProductService ProductService { get; set; } = default!;

    [SupplyParameterFromForm]
    public ProductDto Product { get; set; } = new();
    public List<CategoryDto> SubCategories { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        await CategoryService.GetCategories();
    }
    public async Task GetSubCategories()
    {

        SubCategories = (await CategoryService.GetSubCategories(CategoryId))
            .ToList();
        StateHasChanged();
    }
    public async void SaveProductAsync()
    {       
        await ProductService.SaveProduct(Product);
    }
}