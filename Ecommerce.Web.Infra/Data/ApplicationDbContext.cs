using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Domain.Entities;
using Ecommerce.Web.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
    namespace Ecommerce.Web.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                    : IdentityDbContext<ApplicationUser>(options), IAppDbContext
{
    public DbSet<Product> Products { get;set;}
    public DbSet<Category> Categories { get;set;}
    public DbSet<User> Users { get; set; }
    public DbSet<AppSetting> AppSettings { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }





    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is EntityBase &&
            (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((EntityBase)entityEntry.Entity).ModifiedAt = DateTime.Now;

            if (entityEntry.State == EntityState.Added)
            {
                ((EntityBase)entityEntry.Entity).CreatedAt = DateTime.Now;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AppSetting>()
          .HasKey(e => e.Id);
        builder.Entity<AppSetting>()
           .Property(e => e.Country)
           .IsRequired(); 

        builder.Entity<AppSetting>()
           .HasData(new AppSetting { Id = 1, Country = "USA" , CurrencyName = "USD" ,CurrencySymbol= "$" });
        base.OnModelCreating(builder);
    }
}
