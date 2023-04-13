using Microsoft.AspNetCore.Components;

namespace BlazorShowcase.RazorLib.Shared;

public partial class TopRow : ComponentBase
{
    [Parameter, EditorRequired]
    public Action OpenSettingsDialogOnClick { get; set; } = null!;
}