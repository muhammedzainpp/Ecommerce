using Ecommerce.Web.Domain.Entities;
using Ecommerce.Web.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Web.Application.Interfaces;
public interface IAppDbContext
{
    public DbSet<Product> Products { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<AppSetting> AppSettings { get; set; }
    DbSet<Cart> Carts { get; set; }
    DbSet<CartItem> CartItems { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
