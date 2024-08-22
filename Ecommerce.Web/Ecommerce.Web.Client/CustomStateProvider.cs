using Ecommerce.Web.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Ecommerce.Web.Client;

public class CustomStateProvider(PersistentComponentState state,AppState appState) : PersistentAuthenticationStateProvider(state,appState)
{

}
