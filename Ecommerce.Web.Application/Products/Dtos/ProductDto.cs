using Ecommerce.Web.Application.Categories.Dtos;

namespace Ecommerce.Web.Application.Products.Dtos;
public class ProductDto
{
    public int Id { get; set; }
    public required string Name { get;  set; } 
    public required string Description { get;  set; }
    public string ImageUrl { get; set; } = default!;
    public required CategoryDto Category { get; set; }
}
