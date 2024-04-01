using AttributeInjector.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AttributeInjector.Core;

public class ServiceRegistrar : IServiceRegistrar
{
    private readonly IServiceCollection _services;

    public ServiceRegistrar(IServiceCollection services)
    {
        _services = services;
    }

    public void RegisterServices()
    {
        var services = ServiceMetadataProvider.GetAppServices();

        foreach (var service in services)
        {
            var interfaceType = ServiceMetadataProvider.GetDefaultInterfaceOfService(service);
            var serviceLifetime = ServiceMetadataProvider.GetServiceLifetime(service);

            RegisterService(
                interfaceType,
                service,
                serviceLifetime.GetValueOrDefault(ServiceLifetime.Singleton)
            );
        }
    }

    public void RegisterService(Type? serviceInterface, Type serviceImplementation, ServiceLifetime lifetime)
    {
        var serviceDescriptor = new ServiceDescriptor(
            serviceInterface ?? serviceImplementation,
            serviceImplementation,
            lifetime
        );

        _services.Add(serviceDescriptor);
    }
}