using Ecommerce.Web.Application.Carts.Commands;
using Ecommerce.Web.Application.Categories.Commands;
using Ecommerce.Web.Application.Products.Queries;
using Ecommerce.Web.Client.Shared.Admin.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Apis;

public static class CartApi
{
    public static void MapCarts(this RouteGroupBuilder app)
    {
        var group = app.MapGroup("carts");
        group.MapPost("/", AddToCart);
    }

    private static async Task<IResult> AddToCart([FromServices] IMediator mediatr, [FromBody] CreateCartCommand request)
    {
        var response = await mediatr.Send(request);
        return Results.Ok(response);
    }

    private static async Task<IResult> GetCartItemsByUserId([FromServices] IMediator mediatr, int id)
    {
        var response = await mediatr.Send(new GetProductsByCategoryQuery() { CategoryId = id });
        return Results.Ok(response);
    }
}
