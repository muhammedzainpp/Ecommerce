using Ecommerce.Web.Shared.Common.Dtos;

namespace Ecommerce.Web.Client.Services.Products.Dtos;

public class OutProductDto
{
    public int Id { get; set; }
    public required int ProductId { get; set; }
    public required  string ImageUrl { get; set; }
    public required MoneyDto Price { get; set; } 
}
