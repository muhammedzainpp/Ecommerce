using Microsoft.AspNetCore.Components;

namespace Ecommerce.Web.Client;

public class CustomStateProvider(PersistentComponentState state) : PersistentAuthenticationStateProvider(state)
{

}
