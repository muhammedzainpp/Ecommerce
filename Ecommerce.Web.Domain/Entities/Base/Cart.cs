using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Web.Domain.Entities.Base;
public class Cart : EntityBase
{
    public User User { get; set; } = default!;
    public int UserId { get; set; }
    public List<CartItem> CartItems { get; set; } = default!;
    public static Cart Create(int userId) => new Cart
    {
        UserId = userId
    };
}
