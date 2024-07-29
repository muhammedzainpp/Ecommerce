using Ecommerce.Web.Apis;
using Ecommerce.Web.Application.Common.DI;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Client.Services.Products;
using Ecommerce.Web.Client.Services;
using Ecommerce.Web.Components;
using Ecommerce.Web.Components.Account;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Web.Client.Services.Categories;
using Ecommerce.Web.Data;
using Ecommerce.Web.Exceptions;
using Blazored.Toast;
using Ecommerce.Web.Client.Services.Users;
using Ecommerce.Web.Client.Services.AppSettings;
using Microsoft.AspNetCore.Components;
using Ecommerce.Web.Client.EventSubscribers;
using Ecommerce.Web.Client.Services.Carts;


namespace Ecommerce.Web;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddApplication();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();





        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7290") });
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
        builder.Services.AddScoped<IApiService, ApiService>();
        builder.Services.AddScoped<IProductService, ProductService>();

        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAppSettingsService, AppSettingsService>();
        builder.Services.AddScoped<ICartServices, CartServices>();
        builder.Services.AddScoped<CartEventSubscriber>();

        builder.Services.AddBlazoredToast();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
         .AddIdentityCookies();


        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<IAppDbContext, ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();





        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();


        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Ecommerce.Web.Client._Imports).Assembly);
        app.UseMiddleware(typeof(ErrorHandlingMiddleware));
        var apiGroup = app.MapGroup("/api");
        EndPoints.MapEndPoints(apiGroup);

        // Add additional endpoints required by the Identity /Account Razor components.
        app.MapAdditionalIdentityEndpoints();

        app.Run();
    }
}
