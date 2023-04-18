using BlazorCommon.RazorLib.BackgroundTaskCase;
using BlazorCommon.RazorLib.ComponentRenderers;
using BlazorCommon.RazorLib.Notification;
using BlazorCommon.RazorLib.WatchWindow;
using BlazorCommon.RazorLib.WatchWindow.TreeViewDisplays;
using BlazorTextEditor.RazorLib;
using Fluxor;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorShowcase.RazorLib;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorShowcaseServices(this IServiceCollection services)
    {
        var shouldInitializeFluxor = false;
        
        var watchWindowTreeViewRenderers = new WatchWindowTreeViewRenderers(
            typeof(TreeViewTextDisplay),
            typeof(TreeViewReflectionDisplay),
            typeof(TreeViewPropertiesDisplay),
            typeof(TreeViewInterfaceImplementationDisplay),
            typeof(TreeViewFieldsDisplay),
            typeof(TreeViewExceptionDisplay),
            typeof(TreeViewEnumerableDisplay));
        
        var commonRendererTypes = new BlazorCommonComponentRenderers(
            typeof(BackgroundTaskDisplay),
            typeof(CommonErrorNotificationDisplay),
            typeof(CommonInformativeNotificationDisplay),
            typeof(TreeViewExceptionDisplay),
            typeof(TreeViewMissingRendererFallbackDisplay),
            watchWindowTreeViewRenderers);
        
        // TODO: Move registration of "IBlazorCommonComponentRenderers" to BlazorCommon
        services.AddSingleton<IBlazorCommonComponentRenderers>(_ => commonRendererTypes);

        services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
        services.AddSingleton<IBackgroundTaskMonitor, BackgroundTaskMonitor>();
            
        services.AddHostedService<QueuedHostedService>();
        
        services.AddBlazorTextEditor(options => options with
        {
            InitializeFluxor = shouldInitializeFluxor,
            BlazorCommonOptions = (options.BlazorCommonOptions ?? new()) with
            {
                InitializeFluxor = shouldInitializeFluxor
            }
        });
        
         return services.AddFluxor(options =>
            options.ScanAssemblies(
                typeof(ServiceCollectionExtensions).Assembly,
                typeof(BlazorCommon.RazorLib.ServiceCollectionExtensions).Assembly,
                typeof(BlazorTextEditor.RazorLib.ServiceCollectionExtensions).Assembly));
    }
}