using System.Security.Claims;

namespace Ecommerce.Web.Client.Services.Users;

public class UserService(IApiService apiService) : IUserService
{
    private const string _baseUrl = "api/users";

    private readonly IApiService _apiservice = apiService;

    public async Task CreateUserAsync(CreateUserCommand user)
    {
       await _apiservice.Post(_baseUrl,user);
    }

    public async Task<GetUserDto?> GetUserAsync(ClaimsPrincipal user)
    {
        var response =  await _apiservice.Post<ClaimsPrincipal,GetUserDto>($"{_baseUrl}/user",user);
        return response.Result;
    }
}
public record CreateUserCommand(string UserName, string ApplicationUserId);

public record GetUserDto(int Id, string? UserName, string? ApplicationUserId);
