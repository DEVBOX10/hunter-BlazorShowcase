using BlazorTextEditor.RazorLib.Model;
using Fluxor;

namespace BlazorShowcase.RazorLib.Store.WellKnownModelKindCase;

[FeatureState]
public partial class WellKnownModelKindState
{
    private WellKnownModelKindState()
    {
        WellKnownModelKind = WellKnownModelKind.CSharp;
    }
    
    private WellKnownModelKindState(WellKnownModelKind wellKnownModelKind)
    {
        WellKnownModelKind = wellKnownModelKind;
    }
    
    public WellKnownModelKind WellKnownModelKind { get; }
}