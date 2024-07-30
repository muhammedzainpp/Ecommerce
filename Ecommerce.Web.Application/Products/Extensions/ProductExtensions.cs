using Ecommerce.Web.Application.Categories.Extenions;
using Ecommerce.Web.Application.Common.Extension;
using Ecommerce.Web.Application.Products.Dtos;
using Ecommerce.Web.Domain.Entities;

namespace Ecommerce.Web.Application.Products.Extensions;
public static class ProductExtensions
{
    public static ProductDto ToProductDto(this Product product)
    {
        if (product is null) return null;

        return new ProductDto()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl,    
            Category = product.Category.ToCategoryDto(),
            CategoryId = product.CategoryId,
            Cost = product.Cost.ConvertToMoneyDto(),
            Quantity = product.Quantity
        };
    }
}

