using Ecommerce.Web.Application.Products.Dtos;

namespace Ecommerce.Web.Application.Categories.Dtos;
public class CategoryDto
{
    public required int Id { get; set; }
    public required string Name { get;  set; } = default!;
    public  string? ImageUrl { get; set; }
    public  int? ParentCategoryId { get; set; }
}
