using Blazored.Toast;
using Ecommerce.Web.Client;
using Ecommerce.Web.Client.EventSubscribers;
using Ecommerce.Web.Client.Services;
using Ecommerce.Web.Client.Services.AppSettings;
using Ecommerce.Web.Client.Services.Carts;
using Ecommerce.Web.Client.Services.Categories;
using Ecommerce.Web.Client.Services.Products;
using Ecommerce.Web.Client.Services.Users;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7290") });
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddScoped<IApiService,ApiService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAppSettingsService, AppSettingsService>();
builder.Services.AddScoped<ICartServices, CartServices>();
builder.Services.AddScoped<CartEventSubscriber>();


builder.Services.AddSingleton<AppState>();

builder.Services.AddBlazoredToast();
await builder.Build().RunAsync();
