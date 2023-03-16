using BlazorTextEditor.RazorLib;
using BlazorTextEditor.RazorLib.Diff;
using BlazorTextEditor.RazorLib.Model;
using BlazorTextEditor.RazorLib.ViewModel;
using Microsoft.AspNetCore.Components;

namespace BlazorShowcase.RazorLib.Pages;

public partial class DiffEditorDemoPage : SingleComponentPage
{
    [Inject]
    private ITextEditorService TextEditorService { get; set; } = null!;

    private static readonly TextEditorDiffKey DiffEditorDemoDiffModelKey = TextEditorDiffKey.NewTextEditorDiffKey();
    
    private static readonly TextEditorModelKey DiffEditorDemoBeforeModelKey = TextEditorModelKey.NewTextEditorModelKey();
    private static readonly TextEditorViewModelKey DiffEditorDemoBeforeViewModelKey = TextEditorViewModelKey.NewTextEditorViewModelKey();
    
    private static readonly TextEditorModelKey DiffEditorDemoAfterModelKey = TextEditorModelKey.NewTextEditorModelKey();
    private static readonly TextEditorViewModelKey DiffEditorDemoAfterViewModelKey = TextEditorViewModelKey.NewTextEditorViewModelKey();

    protected override void OnInitialized()
    {
        TextEditorService.ModelRegisterTemplatedModel(
            DiffEditorDemoBeforeModelKey,
            WellKnownModelKind.CSharp,
            "textEditorDemoBefore.txt",
            DateTime.UtcNow,
            "C#",
            "ABCDEFG");
            //TEXT_EDITOR_DEMO_INITIAL_CONTENT);
        
        TextEditorService.ViewModelRegister(
            DiffEditorDemoBeforeViewModelKey,
            DiffEditorDemoBeforeModelKey);

        TextEditorService.ModelRegisterTemplatedModel(
            DiffEditorDemoAfterModelKey,
            WellKnownModelKind.CSharp,
            "textEditorDemoAfter.txt",
            DateTime.UtcNow,
            "C#",
            "ABCDEFG");
            //TEXT_EDITOR_DEMO_INITIAL_CONTENT);
        
        TextEditorService.ViewModelRegister(
            DiffEditorDemoAfterViewModelKey,
            DiffEditorDemoAfterModelKey);
        
        TextEditorService.DiffRegister(
            DiffEditorDemoDiffModelKey,
            DiffEditorDemoBeforeViewModelKey,
            DiffEditorDemoAfterViewModelKey);
        
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