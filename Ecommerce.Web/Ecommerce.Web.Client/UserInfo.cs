namespace Ecommerce.Web.Client;

// Add properties to this class and update the server and client AuthenticationStateProviders
// to expose more information about the authenticated user to the client.
public class UserInfo
{
    public required int UserId { get; set; }
    public required string ApplicationUserId { get; set; }
    public required string Email { get; set; }

}
