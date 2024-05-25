using Ecommerce.Web.Application.Common.Helpers;
using Ecommerce.Web.Application.Interfaces;
using Ecommerce.Web.Client.Shared.Admin.Categories;
using Ecommerce.Web.Components.Account;
using Ecommerce.Web.Data;
using Ecommerce.Web.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecommerce.Web.Apis;

public static class UserApi
{
    public static void MapUsers(this RouteGroupBuilder app)
    {
        var group = app.MapGroup("users");
        group.MapPost("/user", GetUser);
        group.MapPost("/", CreateUser);
    }
    private static async Task<IResult> GetUser( [FromServices]IAppDbContext context, [FromServices] UserManager<ApplicationUser> UserManager)
    {

         var user = UserManager.Users.FirstOrDefault(x=>x.UserName == ClaimsPrincipal.Current.Identity.Name);
        var customUser =await context.Users
            .FirstOrDefaultAsync(x => x.UserName == user.UserName);
        if (customUser is null)
            return Results.Ok(ResponseHelpers.OnError<UserDto>(new Exception("invalid application user id")));

        return Results.Ok(ResponseHelpers.OnSuccess(customUser.MapToDto()));
    }

    private static async Task<IResult> CreateUser([FromServices] IAppDbContext context, [FromBody] UserCommand user)
    {
        var userEntity = user.MapToEntity();
        await context.Users.AddAsync(userEntity);
        await context.SaveChangesAsync();

        return Results.Ok(ResponseHelpers.OnSuccess(userEntity.Id));
    }
}

public record UserDto(int Id ,string? UserName , string? ApplicationUserId );


public static class UserExtensions
{

    public static UserDto MapToDto(this User user)
    {
        return new UserDto(user.Id, user.UserName, user.ApplicationUserId);
    }

    public static User MapToEntity(this UserCommand user)
    {
        return User.Create(user.UserName,user.ApplicationUserId);
    }
}

public record UserCommand(string UserName , string ApplicationUserId);



