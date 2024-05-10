using Ecommerce.Web.Client.Services.Products;
using Ecommerce.Web.Client.Services.Products.Dtos;
using Microsoft.AspNetCore.Components;

namespace Ecommerce.Web.Client.Shared;

public partial class ProductVariantList
{
    [Inject]
    public IProductService ProductService { get; set; } = default!;
    [Parameter]
    public int ProductId { get; set; } 

    [Parameter]
    public int CategoryId { get; set; }
    public ProductDto Product { get; set; } = new ProductDto();

    protected override async Task OnParametersSetAsync()
    {
       Product = await ProductService.GetProduct(ProductId);
    }


}
