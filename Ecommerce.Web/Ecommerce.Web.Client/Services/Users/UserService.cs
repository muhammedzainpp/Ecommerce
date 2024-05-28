using System.Security.Claims;

namespace Ecommerce.Web.Client.Services.Users;

public class UserService(IApiService apiService) : IUserService
{
    private const string _baseUrl = "api/users";

    private readonly IApiService _apiservice = apiService;

    public async Task CreateUserAsync(CreateUserCommand user)
    {
        await _apiservice.Post(_baseUrl, user);
    }

    public async Task<GetUserDto?> GetUserAsync(string applicationUserId)
    {
        var response = await _apiservice.Get<GetUserDto>($"{_baseUrl}?applicationUserId={applicationUserId}");
        return response.Result;
    }
}
public record CreateUserCommand(string UserName, string ApplicationUserId);

public record GetUserDto(int Id, string? UserName, string? ApplicationUserId);
