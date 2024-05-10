using Ecommerce.Web.Application.Categories.Dtos;
using Ecommerce.Web.Domain.Entities;

namespace Ecommerce.Web.Application.Categories.Extenions;
public static  class CategoryExtensions
{
    public static CategoryDto ToCategoryDto(this Category category)
    {
        return new CategoryDto()
        {
            Id = category.Id,
            Name = category.Name,
            ImageUrl = category.ImageUrl,   
        };
    }
}
