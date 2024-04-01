using Microsoft.Extensions.DependencyInjection;

namespace AttributeInjector.Core;

public static class AttributeInjectorExtensions
{
    public static void AddAttributeInjector(this IServiceCollection services)
    {
        services.AddSingleton(new ServiceRegistrar(services));

        var serviceRegistrar = services.BuildServiceProvider().GetService<ServiceRegistrar>();
        serviceRegistrar?.RegisterServices();
    }
}