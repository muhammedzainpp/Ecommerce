using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Web.Domain.Entities.Base;
public class Cart : EntityBase
{
    public User User { get; set; } = default!;
    [ForeignKey("User")]
    public new int Id { get; set; }
    public List<CartItem> CartItems { get; set; } = default!;

    public static Cart Create(int userId) => new Cart
    {
        Id = userId
    };
}
