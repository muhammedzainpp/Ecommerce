namespace Ecommerce.Web.Apis;

public static class EndPoints
{
    public static void MapEndPoints(this RouteGroupBuilder app)
    {
        app.MapProducts();
        app.MapCategories();
        app.MapUsers();
        app.MapAppSettings();
        app.MapCarts();
	}
}
