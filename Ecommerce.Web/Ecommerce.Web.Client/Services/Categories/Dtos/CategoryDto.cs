namespace Ecommerce.Web.Client.Services.Categories.Dtos;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public int? ParentCategoryId { get; set; }
}
