using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Ecommerce.Web.Components.Layout;

public partial class MainLayout
{
    //[Inject]
    //public IJSRuntime JsRuntime { get; set; } = default!;



    private bool isNavOpen = false;

    private string navDisplay => isNavOpen ? "block" : "none";

    private void ToggleNav()
    {
        isNavOpen = !isNavOpen;
    }
    //public async Task OpenNav()
    //{
    //    await JsRuntime.InvokeVoidAsync("openNav");

    //}
    //public async Task CloseNav()
    //{
    //    await JsRuntime.InvokeVoidAsync("closeNav");

    //}
}
