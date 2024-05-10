
using Ecommerce.Web.Application.OutProducts.Commands;
using Ecommerce.Web.Application.OutProducts.Queries;
using Ecommerce.Web.Application.Products.Commands;
using Ecommerce.Web.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Apis;

public static class ProductApi
{
    public static void MapProducts(this RouteGroupBuilder app)
    {
        var group = app.MapGroup("products"); 
        group.MapPost("/",SaveProducts);
        group.MapGet("/", GetProducts);
        group.MapGet("/GetById/{id}", GetProduct);
        group.MapGet("/GetByCategory/{id}", GetProductsByCategory);
        group.MapPost("SaveOutProduct",SaveOutProduct);
        group.MapGet("/GetOutProducts/{id}", GetOutProducts);


    }
    private static async Task<IResult> GetProducts([FromServices] IMediator mediatr)
    {
        var response = await mediatr.Send(new GetProductsQuery());
        return Results.Ok(response);
    }

    private static async Task<IResult> GetProduct([FromServices] IMediator mediatr , string id)
    {
        var isSuccess = int.TryParse(id, out var requestId);
        var response = await mediatr.Send(new GetProductQuery (){Id = requestId});
        return Results.Ok(response);
    }

    private static async Task<IResult> SaveProducts([FromServices] IMediator mediatr,[FromBody] SaveProductCommand request)
    {
        var response = await mediatr.Send(request);
        return Results.Ok(response);
    }
    private static async Task<IResult> SaveOutProduct([FromServices] IMediator mediatr, [FromBody] SaveOutProductCommand request)
    {
        var response = await mediatr.Send(request);
        return Results.Ok(response);
    }

    private static async Task<IResult> GetProductsByCategory([FromServices] IMediator mediatr , int id)
    {
        var response = await mediatr.Send(new GetOutProductsByBaseProductQuery() { CategoryId = id});
        return Results.Ok(response);
    }
    private static async Task<IResult> GetOutProducts([FromServices] IMediator mediatr, int id)
    {
        var response = await mediatr.Send(new GetOutProductsByBaseProductCommand() { ProductId = id });
        return Results.Ok(response);
    }
}
