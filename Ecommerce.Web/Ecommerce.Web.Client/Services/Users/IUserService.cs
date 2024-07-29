
using System.Security.Claims;

namespace Ecommerce.Web.Client.Services.Users;

public interface IUserService
{
    Task CreateUserAsync(CreateUserCommand user);
    Task<GetUserDto?> GetUserAsync(string applicationUserId);
}