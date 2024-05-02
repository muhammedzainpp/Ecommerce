using Ecommerce.Web.Domain.ValueObjects;

namespace Ecommerce.Web.Application.Products.Dtos;
public class ProductDto
{
    public int Id { get; set; }
    public string Title { get;  set; } = default!;
    public string Description { get;  set; } = default!;
    public string ImageUrl { get;  set; } = default!;
    public Money Price { get;  set; } = default!;
    public int CategoryId { get; set; }
}
