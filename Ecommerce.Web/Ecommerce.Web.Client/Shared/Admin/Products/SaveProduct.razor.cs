using Ecommerce.Web.Client.Services.Categories;
using Ecommerce.Web.Client.Services.Categories.Dtos;
using Ecommerce.Web.Client.Services.Products;
using Ecommerce.Web.Client.Services.Products.Dtos;
using Microsoft.AspNetCore.Components;

namespace Ecommerce.Web.Client.Shared.Admin.Products;

public partial class SaveProduct
{

    [Inject]
    public ICategoryService CategoryService { get; set; } = default!;
    [Inject]
    public IProductService ProductService { get; set; } = default!;

    public int CategoryId { get; set; }

    [SupplyParameterFromForm]
    public ProductDto Product { get; set; } = new ProductDto();
    public IEnumerable<CategoryDto> SubCategories { get; set; } = new List<CategoryDto>();
    protected override async Task OnInitializedAsync()
    {
        await CategoryService.GetCategories();
    }
    public async void GetSubCategories(int parentcategoryId)
    {
        SubCategories = await CategoryService.GetSubCategories(parentcategoryId);
    }
    public async Task SaveProductAsync()
    {
        await ProductService.SaveProduct(Product);
    }
}
