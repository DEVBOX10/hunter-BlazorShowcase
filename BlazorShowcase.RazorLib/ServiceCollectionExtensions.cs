using BlazorCommon.RazorLib;
using BlazorTextEditor.RazorLib;
using Fluxor;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorShowcase.RazorLib;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorShowcaseServices(this IServiceCollection services)
    {
        // TODO: Standardize the way options are modified. AddBlazorCommonServices is a record yet AddBlazorTextEditor is a settable class 
        services.AddBlazorCommonServices(options => options with
        {
            InitializeFluxor = false
        });

        services.AddBlazorTextEditor(options => 
            options.InitializeFluxor = false);
        
        return services.AddFluxor(options => 
            options.ScanAssemblies(
                typeof(ServiceCollectionExtensions).Assembly,
                typeof(BlazorCommon.RazorLib.ServiceCollectionExtensions).Assembly,
                typeof(BlazorTextEditor.RazorLib.ServiceCollectionExtensions).Assembly));
    }
}