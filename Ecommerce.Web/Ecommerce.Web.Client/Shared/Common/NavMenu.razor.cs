using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Ecommerce.Web.Client.Shared;

public partial class NavMenu
{
    [Inject]
    public IJSRuntime JS { get; set; } = default!;
    public async Task OpenNav()
    {
        await JS.InvokeVoidAsync("openNav");

    }
    public async Task CloseNav()
    {
        await JS.InvokeVoidAsync("closeNav");

    }
}
