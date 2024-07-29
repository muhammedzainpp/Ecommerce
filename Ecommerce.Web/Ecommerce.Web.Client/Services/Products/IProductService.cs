using Ecommerce.Web.Client.Services.Products.Dtos;

namespace Ecommerce.Web.Client.Services.Products;

public interface IProductService
{
    IEnumerable<ProductDto> Products { get; set; }

    //Task<IEnumerable<ItemDto>> GetOutProductsByBaseProduct(int productId);
    Task<ProductDto> GetProduct(int id);
    Task GetProducts();
    Task<IEnumerable<ProductDto>> GetProductsByCategory(int categoryId);
    Task<int> SaveProduct(ProductDto product);
    //Task GetProductsByCategory(int categoryId);
}
