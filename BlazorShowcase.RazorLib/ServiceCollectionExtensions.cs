using BlazorTextEditor.RazorLib;
using Fluxor;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorShowcase.RazorLib;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorShowcaseServices(this IServiceCollection services)
    {
        var shouldInitializeFluxor = false;
        
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