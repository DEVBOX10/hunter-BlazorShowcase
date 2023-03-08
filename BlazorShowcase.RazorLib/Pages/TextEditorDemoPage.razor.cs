using BlazorTextEditor.RazorLib;
using BlazorTextEditor.RazorLib.Model;
using BlazorTextEditor.RazorLib.ViewModel;
using Microsoft.AspNetCore.Components;

namespace BlazorShowcase.RazorLib.Pages;

public partial class TextEditorDemoPage : ComponentBase
{
    [Inject]
    private ITextEditorService TextEditorService { get; set; } = null!;

    private static readonly TextEditorModelKey TextEditorDemoModelKey = TextEditorModelKey.NewTextEditorModelKey();
    private static readonly TextEditorViewModelKey TextEditorDemoViewModelKey = TextEditorViewModelKey.NewTextEditorViewModelKey();

    private const string LINE_HEIGHT_OF_H3 = "1.2";
    private const string HEIGHT_OF_H3 = $"calc(calc(1.3rem + .6vw) * {LINE_HEIGHT_OF_H3})";
    private const string HEIGHT_OF_HR = "1px";
    private const string TOTAL_VERTICAL_MARGIN_OF_HR = "2rem";
    
    private string CssStyleTextEditorViewModelDisplay =>
        $"height: calc(100% - {HEIGHT_OF_H3} - {HEIGHT_OF_HR} - {TOTAL_VERTICAL_MARGIN_OF_HR});";
    
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