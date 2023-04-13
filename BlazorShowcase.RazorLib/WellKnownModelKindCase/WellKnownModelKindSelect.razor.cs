using BlazorShowcase.RazorLib.Store.WellKnownModelKindCase;
using BlazorTextEditor.RazorLib.Model;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorShowcase.RazorLib.WellKnownModelKindCase;

public partial class WellKnownModelKindSelect : FluxorComponent
{
    [Inject]
    private IState<WellKnownModelKindState> WellKnownModelKindStateWrap { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    private bool CheckIsWellKnownModelKindSelected(
        string wellKnownModelKindString,
        WellKnownModelKindState localWellKnownModelKindStateWrap)
    {
        return wellKnownModelKindString ==
               localWellKnownModelKindStateWrap.WellKnownModelKind.ToString();
    }
    
    private void OnSelectedWellKnownModelKindChanged(ChangeEventArgs changeEventArgs)
    {
        var wellKnownModelKindString = changeEventArgs.Value as string;

        if (string.IsNullOrWhiteSpace(wellKnownModelKindString))
            return;

        if (Enum.TryParse<WellKnownModelKind>(wellKnownModelKindString, out var wellKnownModelKind))
        {
            Dispatcher.Dispatch(new WellKnownModelKindState.SetWellKnownModelKindAction(
                wellKnownModelKind));
        }
    }
}