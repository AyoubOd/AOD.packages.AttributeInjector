using Microsoft.Extensions.DependencyInjection;

namespace AttributeInjector.Core;

[AttributeUsage(AttributeTargets.Class)]
public class ServiceAttribute : Attribute
{
    public ServiceAttribute()
    {
        Lifetime = ServiceLifetime.Singleton;
    }

    public ServiceAttribute(ServiceLifetime lifetime)
    {
        Lifetime = lifetime;
    }

    public ServiceLifetime Lifetime { get; set; }
}