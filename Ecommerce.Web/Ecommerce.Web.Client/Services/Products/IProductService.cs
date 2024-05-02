using Ecommerce.Web.Client.Services.Products.Dtos;

namespace Ecommerce.Web.Client.Services.Products;

public interface IProductService
{
    IEnumerable<ProductDto> Products { get; set; }


    Task<ProductDto> GetProduct(int id);
    Task GetProducts();
    Task GetProductsByCategory(int categoryId);
}
