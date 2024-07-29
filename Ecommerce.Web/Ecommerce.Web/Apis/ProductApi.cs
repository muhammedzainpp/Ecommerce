
using Ecommerce.Web.Application.Products.Commands;
using Ecommerce.Web.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;

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
        group.MapGet("/GetProducts/{id}", GetProducts);
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
    private static async Task<IResult> GetProductsByCategory([FromServices] IMediator mediatr , int id)
    {
        var response = await mediatr.Send(new GetProductsByCategoryQuery() { CategoryId = id});
        return Results.Ok(response);
    }

 

}
