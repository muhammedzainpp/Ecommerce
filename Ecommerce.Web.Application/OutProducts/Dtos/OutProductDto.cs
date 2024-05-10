using Ecommerce.Web.Domain.Entities.Base;
using Ecommerce.Web.Domain.ValueObjects;

namespace Ecommerce.Web.Application.OutProducts.Dtos;
public class OutProductDto 
{
    public int Id { get; set; }
    public required int ProductId { get; set; }
    public  required string ImageUrl { get; set; } 
    public required Money Price { get;  set; } 
}

