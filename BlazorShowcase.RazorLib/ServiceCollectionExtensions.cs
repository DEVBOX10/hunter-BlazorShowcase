using BlazorCommon.RazorLib;
using Fluxor;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorShowcase.RazorLib;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorShowcaseServices(this IServiceCollection services)
    {
        services.AddFluxor(options => 
            options.ScanAssemblies(
                typeof(ServiceCollectionExtensions).Assembly,
                typeof(BlazorCommon.RazorLib.ServiceCollectionExtensions).Assembly));
        
        return services
            .AddBlazorCommonServices(options => options with
            {
                InitializeFluxor = false
            });
    }
}