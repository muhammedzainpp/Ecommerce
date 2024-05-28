using Ecommerce.Web.Domain.Entities.Base;

namespace Ecommerce.Web.Domain.Entities;
public class Cart : EntityBase
{
    public User User { get; set; } = default!;
    public int UserId { get; set; }
}
