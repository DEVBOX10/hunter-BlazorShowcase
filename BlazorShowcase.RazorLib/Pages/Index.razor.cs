using BlazorShowcase.RazorLib.Store.WellKnownModelKindCase;
using BlazorTextEditor.RazorLib;
using BlazorTextEditor.RazorLib.Analysis.CSharp.SyntaxActors;
using BlazorTextEditor.RazorLib.Analysis.Css.Decoration;
using BlazorTextEditor.RazorLib.Analysis.Css.SyntaxActors;
using BlazorTextEditor.RazorLib.Analysis.FSharp.SyntaxActors;
using BlazorTextEditor.RazorLib.Analysis.GenericLexer.Decoration;
using BlazorTextEditor.RazorLib.Analysis.Html.Decoration;
using BlazorTextEditor.RazorLib.Analysis.Html.SyntaxActors;
using BlazorTextEditor.RazorLib.Analysis.JavaScript.SyntaxActors;
using BlazorTextEditor.RazorLib.Analysis.Json.Decoration;
using BlazorTextEditor.RazorLib.Analysis.Json.SyntaxActors;
using BlazorTextEditor.RazorLib.Analysis.Razor.SyntaxActors;
using BlazorTextEditor.RazorLib.Analysis.TypeScript.SyntaxActors;
using BlazorTextEditor.RazorLib.Decoration;
using BlazorTextEditor.RazorLib.Lexing;
using BlazorTextEditor.RazorLib.Model;
using BlazorTextEditor.RazorLib.ViewModel;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorShowcase.RazorLib.Pages;

public partial class Index : SingleComponentPage
{
    [Inject]
    private ITextEditorService TextEditorService { get; set; } = null!;
    [Inject]
    private IState<WellKnownModelKindState> WellKnownModelKindStateWrap { get; set; } = null!;

    private static readonly TextEditorModelKey TextEditorDemoModelKey = TextEditorModelKey.NewTextEditorModelKey();
    private static readonly TextEditorViewModelKey TextEditorDemoViewModelKey = TextEditorViewModelKey.NewTextEditorViewModelKey();

    private bool _disposed;
    
    protected override void OnInitialized()
    {
        WellKnownModelKindStateWrap.StateChanged += WellKnownModelKindStateWrapOnStateChanged;
        
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

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            WellKnownModelKindStateWrapOnStateChanged(null, EventArgs.Empty);
        }
        
        await base.OnAfterRenderAsync(firstRender);
    }

    private async void WellKnownModelKindStateWrapOnStateChanged(object? sender, EventArgs e)
    {
        // Capture the mutable references locally first thing 
        
        var localWellKnownModelState = WellKnownModelKindStateWrap.Value;
        
        var demoModel = TextEditorService
            .ModelFindOrDefault(TextEditorDemoModelKey);
        
        ILexer? lexer = null;
        IDecorationMapper? decorationMapper = null;
        
        switch (localWellKnownModelState.WellKnownModelKind)
        {
            case WellKnownModelKind.CSharp:
                lexer = new TextEditorCSharpLexer();
                decorationMapper = new GenericDecorationMapper();
                break;
            case WellKnownModelKind.Html:
                lexer = new TextEditorHtmlLexer();
                decorationMapper = new TextEditorHtmlDecorationMapper();
                break;
            case WellKnownModelKind.Css:
                lexer = new TextEditorCssLexer();
                decorationMapper = new TextEditorCssDecorationMapper();
                break;
            case WellKnownModelKind.Json:
                lexer = new TextEditorJsonLexer();
                decorationMapper = new TextEditorJsonDecorationMapper();
                break;
            case WellKnownModelKind.FSharp:
                lexer = new TextEditorFSharpLexer();
                decorationMapper = new GenericDecorationMapper();
                break;
            case WellKnownModelKind.Razor:
                lexer = new TextEditorRazorLexer();
                decorationMapper = new TextEditorHtmlDecorationMapper();
                break;
            case WellKnownModelKind.JavaScript:
                lexer = new TextEditorJavaScriptLexer();
                decorationMapper = new GenericDecorationMapper();
                break;
            case WellKnownModelKind.TypeScript:
                lexer = new TextEditorTypeScriptLexer();
                decorationMapper = new GenericDecorationMapper();
                break;
        }

        if (demoModel is not null)
        {
            demoModel.SetLexer(lexer);
            demoModel.SetDecorationMapper(decorationMapper);
            
            await demoModel.ApplySyntaxHighlightingAsync();
        }
    }
    
    protected override void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }
    
        if (disposing)
        {
            WellKnownModelKindStateWrap.StateChanged -= WellKnownModelKindStateWrapOnStateChanged;
        }
    
        _disposed = true;
        
        base.Dispose(disposing);
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