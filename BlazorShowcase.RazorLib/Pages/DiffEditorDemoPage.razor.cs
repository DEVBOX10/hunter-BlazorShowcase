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
using BlazorTextEditor.RazorLib.Diff;
using BlazorTextEditor.RazorLib.Lexing;
using BlazorTextEditor.RazorLib.Model;
using BlazorTextEditor.RazorLib.ViewModel;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorShowcase.RazorLib.Pages;

public partial class DiffEditorDemoPage : SingleComponentPage
{
    [Inject]
    private ITextEditorService TextEditorService { get; set; } = null!;
    [Inject]
    private IState<WellKnownModelKindState> WellKnownModelKindStateWrap { get; set; } = null!;

    private static readonly TextEditorDiffKey DiffEditorDemoDiffModelKey = TextEditorDiffKey.NewTextEditorDiffKey();
    
    private static readonly TextEditorModelKey DiffEditorDemoBeforeModelKey = TextEditorModelKey.NewTextEditorModelKey();
    private static readonly TextEditorViewModelKey DiffEditorDemoBeforeViewModelKey = TextEditorViewModelKey.NewTextEditorViewModelKey();
    
    private static readonly TextEditorModelKey DiffEditorDemoAfterModelKey = TextEditorModelKey.NewTextEditorModelKey();
    private static readonly TextEditorViewModelKey DiffEditorDemoAfterViewModelKey = TextEditorViewModelKey.NewTextEditorViewModelKey();

    private bool _disposed;
    
    protected override void OnInitialized()
    {
        WellKnownModelKindStateWrap.StateChanged += WellKnownModelKindStateWrapOnStateChanged;        
        
        TextEditorService.Model.RegisterTemplated(
            DiffEditorDemoBeforeModelKey,
            WellKnownModelKind.CSharp,
            "textEditorDemoBefore.txt",
            DateTime.UtcNow,
            "C#",
            "ABCDEFK");
            //TEXT_EDITOR_DEMO_INITIAL_CONTENT);
        
        TextEditorService.ViewModel.Register(
            DiffEditorDemoBeforeViewModelKey,
            DiffEditorDemoBeforeModelKey);

        TextEditorService.Model.RegisterTemplated(
            DiffEditorDemoAfterModelKey,
            WellKnownModelKind.CSharp,
            "textEditorDemoAfter.txt",
            DateTime.UtcNow,
            "C#",
            "BHDEFCK");
            //TEXT_EDITOR_DEMO_INITIAL_CONTENT);
        
        TextEditorService.ViewModel.Register(
            DiffEditorDemoAfterViewModelKey,
            DiffEditorDemoAfterModelKey);
        
        TextEditorService.Diff.Register(
            DiffEditorDemoDiffModelKey,
            DiffEditorDemoBeforeViewModelKey,
            DiffEditorDemoAfterViewModelKey);
        
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
        
        var beforeModel = TextEditorService
            .Model.FindOrDefault(DiffEditorDemoBeforeModelKey);
        
        var afterModel = TextEditorService
            .Model.FindOrDefault(DiffEditorDemoAfterModelKey);
        
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

        if (beforeModel is not null)
        {
            beforeModel.SetLexer(lexer);
            beforeModel.SetDecorationMapper(decorationMapper);
            
            await beforeModel.ApplySyntaxHighlightingAsync();
        }
        
        if (afterModel is not null)
        {
            afterModel.SetLexer(lexer);
            afterModel.SetDecorationMapper(decorationMapper);
            
            await afterModel.ApplySyntaxHighlightingAsync();
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