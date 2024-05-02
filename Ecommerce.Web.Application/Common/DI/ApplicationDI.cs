using Ecommerce.Web.Application.Products.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Web.Application.Common.DI;
public static class ApplicationDI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
                   cfg.RegisterServicesFromAssembly(typeof(SaveProductCommand).Assembly));
        return services;
    }
}
