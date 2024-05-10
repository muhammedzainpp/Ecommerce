using Ecommerce.Web.Client.Services.Products.Dtos;

namespace Ecommerce.Web.Client.Services.Products;

public interface IProductService
{
    IEnumerable<ProductDto> Products { get; set; }

    Task<IEnumerable<OutProductDto>> GetOutProductsByBaseProduct(int productId);
    Task<ProductDto> GetProduct(int id);
    Task GetProducts();
    Task<IEnumerable<ProductDto>> GetProductsByCategory(int categoryId);
    //Task GetProductsByCategory(int categoryId);
}
