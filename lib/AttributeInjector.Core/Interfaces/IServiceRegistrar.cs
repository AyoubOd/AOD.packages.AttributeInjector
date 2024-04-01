using Microsoft.Extensions.DependencyInjection;

namespace AttributeInjector.Core.Interfaces;

public interface IServiceRegistrar
{
    void RegisterService(
        Type? serviceInterface,
        Type serviceImplementation,
        ServiceLifetime lifetime
    );

    void RegisterServices();
}