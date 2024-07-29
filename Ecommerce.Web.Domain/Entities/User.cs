using Ecommerce.Web.Domain.Entities.Base;

namespace Ecommerce.Web.Domain.Entities;
public class User : EntityBase
{
    public string UserName { get; private set; } = default!;
    public string ApplicationUserId { get; private set; } = default!;

    public static User Create(string userName, string applicationUserId) 
        => new () 
        { 
            UserName = userName,
            ApplicationUserId = applicationUserId 
        };
}
