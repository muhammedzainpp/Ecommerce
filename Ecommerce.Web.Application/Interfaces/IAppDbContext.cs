using Ecommerce.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Web.Application.Interfaces;
public interface IAppDbContext
{
    public DbSet<Product> Products { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<OutProduct> OutProducts { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
