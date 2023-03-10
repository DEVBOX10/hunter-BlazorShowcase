using BlazorTextEditor.RazorLib;
using BlazorTextEditor.RazorLib.Model;
using BlazorTextEditor.RazorLib.ViewModel;
using Microsoft.AspNetCore.Components;

namespace BlazorShowcase.RazorLib.Pages;

public partial class TextEditorDemoPage : SingleComponentPage
{
    [Inject]
    private ITextEditorService TextEditorService { get; set; } = null!;

    private static readonly TextEditorModelKey TextEditorDemoModelKey = TextEditorModelKey.NewTextEditorModelKey();
    private static readonly TextEditorViewModelKey TextEditorDemoViewModelKey = TextEditorViewModelKey.NewTextEditorViewModelKey();

    protected override void OnInitialized()
    {
        TextEditorService.ModelRegisterTemplatedModel(
            TextEditorDemoModelKey,
            WellKnownModelKind.CSharp,
            "textEditorDemo.txt",
            DateTime.UtcNow,
            "C#",
            TEXT_EDITOR_DEMO_INITIAL_CONTENT);
        
        TextEditorService.ViewModelRegister(
            TextEditorDemoViewModelKey,
            TextEditorDemoModelKey);
        
        base.OnInitialized();
    }

    private const string TEXT_EDITOR_DEMO_INITIAL_CONTENT = @"public class MyClass
{
    public List<int> _myInts = new()
    {
        1,
        2,
        3,
    };
    
    public void MyMethod()
    {
        // A comment

        var intValue = 2;
        var stringValue = ""Hello World!"";

        return;
    }
}";
}