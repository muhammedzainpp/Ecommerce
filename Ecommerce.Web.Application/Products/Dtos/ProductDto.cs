using Ecommerce.Web.Application.Categories.Dtos;
using Ecommerce.Web.Shared.Common.Dtos;

namespace Ecommerce.Web.Application.Products.Dtos;
public class ProductDto
{
    public int Id { get; set; }
    public required string Name { get;  set; } 
    public required string Description { get;  set; }
    public string? ImageUrl { get; set; } 
    public required CategoryDto Category { get; set; }
    public int CategoryId { get; set; }
    public required MoneyDto Cost { get; set; }
    public int Quantity { get; set; }

}
