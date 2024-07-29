using Ecommerce.Web.Client.Services.Products.Dtos;

namespace Ecommerce.Web.Client.Services.Products;

public class ProductService(IApiService apiservice) : IProductService
{
    private const string _baseUrl = "api/products";
    private readonly IApiService _apiservice = apiservice;


    public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();
    public async Task GetProducts()
    {
        var response = await _apiservice.Get<IEnumerable<ProductDto>>(_baseUrl);
        if(response != null && response.Result != null)
        Products = response.Result;
    }
    public async Task<ProductDto> GetProduct(int id)
    {
        var response = await _apiservice.GetById<ProductDto>($"{_baseUrl}/GetById",id);
        return response.Result ?? default!;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCategory(int categoryId)
    {
        var response = await _apiservice.GetById<IEnumerable<ProductDto>>($"{_baseUrl}/GetByCategory",categoryId);
        if (response != null && response.Result != null)
            return response.Result;
        else
            throw new Exception();       
    }


    public async Task<int> SaveProduct(ProductDto product)
    {
        var response = await _apiservice.Post($"{_baseUrl}/", product);
        if (response != null )
            return response.Result;
        else
            throw new Exception();
    }





}

    //public async Task<IEnumerable<ProductDto>> GetAllProducts(string productname)
    //{
    //    var response = await _apiservice.GetById<IEnumerable<ProductDto>>($"{_baseUrl}/GetByCategory", categoryId);
    //    if (response != null && response.Result != null)
    //        return response.Result;
    //    else
    //        throw new Exception();
    //}



    //public async Task<IEnumerable<OutProductDto>> GetOutProductsByBaseProduct(int productId)
    //{
    //    var response = await _apiservice.GetById<IEnumerable<OutProductDto>>($"{_baseUrl}/GetOutProducts", productId);
    //    if (response != null && response.Result != null)
    //        return response.Result;
    //    else
    //        throw new Exception();
    //}