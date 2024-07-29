using Ecommerce.Web.Application.AppSettings.Commands;
using Ecommerce.Web.Application.AppSettings.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Apis;

public static class AppSettingsApi
{
    public static void MapAppSettings(this RouteGroupBuilder app)
    {
        var group = app.MapGroup("appsettings");
        group.MapPost("/", SaveAppSettings);
        group.MapGet("/", GetAppSettings);
    }

    private static async Task<IResult> SaveAppSettings([FromServices] IMediator mediatr, [FromBody] SaveAppsettingsCommand request)
    {
        var response = await mediatr.Send(request);
        return Results.Ok(response);
    }

    private static async Task<IResult> GetAppSettings([FromServices] IMediator mediatr)
    {
        var response = await mediatr.Send(new GetAppSettingsQuery());
        return Results.Ok(response);
    }
}
