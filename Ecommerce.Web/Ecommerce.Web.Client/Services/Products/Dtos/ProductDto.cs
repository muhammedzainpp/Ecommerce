using Ecommerce.Web.Shared.Common.Dtos;

namespace Ecommerce.Web.Client.Services.Products.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public MoneyDto Price { get; set; } = new MoneyDto();
    public int CategoryId { get; set; }
}
