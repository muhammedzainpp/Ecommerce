
using Ecommerce.Web.Application.Categories.Commands;
using Ecommerce.Web.Application.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Apis;

public static class CategoryApi
{
    public static void MapCategories(this RouteGroupBuilder app)
    {
        var group = app.MapGroup("categories");
        group.MapPost("/", SaveCategory);
        group.MapGet("/", GetCategories);
        group.MapGet("/GetSubCategories/{id}",GetSubCategories);
    }

    private static async Task<IResult> SaveCategory([FromServices] IMediator mediatr, [FromBody] SaveCategoryCommand request)
    {

        var response = await mediatr.Send(request);
        return Results.Ok(response);  
    }
    private static async Task<IResult> GetCategories([FromServices] IMediator mediatr)
    {
        var response = await mediatr.Send(new GetCategoriesQuery());
        return Results.Ok(response);
    }
    private static async Task<IResult> GetSubCategories([FromServices] IMediator mediatr ,int id)
    {
        var response = await mediatr.Send(new GetSubCategoriesQuery() { ParentCategoryId = id});
        return Results.Ok(response);
    }
}
