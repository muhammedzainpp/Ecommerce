using Ecommerce.Web.Client.Services.Products;
using Microsoft.AspNetCore.Components;


namespace Ecommerce.Web.Client.Shared;

public partial class ProductList
{
    [Parameter]
    public int CategoryId { get; set; }

    [Inject]
    public required IProductService ProductService { get; set; } 
    protected override async Task OnParametersSetAsync()
    
    {
        if(CategoryId == 0)
            await ProductService.GetProducts();
        else
            await ProductService.GetProductsByCategory(CategoryId);

    }

 
    public void GetById(int id)
    {
        navigationManager.NavigateTo($"product/{id}");
    }
    
}
