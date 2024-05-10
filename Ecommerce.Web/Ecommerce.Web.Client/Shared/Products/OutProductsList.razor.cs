using Ecommerce.Web.Client.Services.Products;
using Ecommerce.Web.Client.Services.Products.Dtos;
using Microsoft.AspNetCore.Components;

namespace Ecommerce.Web.Client.Shared.Products;

public partial class OutProductsList
{
    [Inject]
    public IProductService ProductService { get; set; } = default!;
    [Parameter]
    public int ProductId { get; set; }

    public IEnumerable<OutProductDto> OutProducts { get; set; } = new List<OutProductDto>();

    protected override async Task OnParametersSetAsync()
    {
        OutProducts = await ProductService.GetOutProductsByBaseProduct(ProductId);
    }
}
