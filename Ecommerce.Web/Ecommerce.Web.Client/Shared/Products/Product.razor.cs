using Ecommerce.Web.Client.Services.Products;
using Ecommerce.Web.Client.Services.Products.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Ecommerce.Web.Client.Shared;

public partial class Product
{
    [Inject]
    public IProductService ProductService { get; set; } = default!;

    [Parameter]
    public int ProductId { get; set; }

    [Inject]
    public IJSRuntime _jsRunTime { get; set; } = default!;

    public ProductDto ProductWithGivenId { get; set; } = new ProductDto() {Name =string.Empty,Description=string.Empty,Cost = new()};

    //protected override async Task OnParametersSetAsync()
    //{
    //    ProductWithGivenId = await ProductService.GetProduct(ProductId);
    //}


}
