using BlazorTextEditor.RazorLib.Model;

namespace BlazorShowcase.RazorLib.Store.WellKnownModelKindCase;

public partial class WellKnownModelKindState
{
    public record SetWellKnownModelKindAction(WellKnownModelKind WellKnownModelKind);
}