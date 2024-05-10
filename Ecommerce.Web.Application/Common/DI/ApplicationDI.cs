using Ecommerce.Web.Application.Products.Commands;
using Ecommerce.Web.Application.ValidationBehaviour;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Ecommerce.Web.Application.Common.DI;
public static class ApplicationDI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddValidatorsFromAssembly(typeof(ApplicationDI).Assembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(SaveProductCommand).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });     
        return services;
    }
}
