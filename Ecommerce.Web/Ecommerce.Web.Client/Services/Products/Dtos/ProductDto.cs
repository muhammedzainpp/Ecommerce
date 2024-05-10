namespace Ecommerce.Web.Client.Services.Products.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public int CategoryId { get; set; }
}
