﻿using Ecommerce.Web.Client.Services.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Ecommerce.Web.Client.Services;

public class AppState
{
    public int UserId { get; set; }

    [Inject]
    public required IUserService UserService { get; set; }
    public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private async Task<int?> GetCurrentUserId()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        if (authState is not null && authState.User.Identity is not null)
        {
            var userId = authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var loggedInUser = await UserService.GetUserAsync(userId);
            var currentUserId = loggedInUser?.Id;

            return currentUserId;
        }

        return null;

    }

}
