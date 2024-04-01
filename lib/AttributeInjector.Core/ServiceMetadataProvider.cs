using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AttributeInjector.Core;

public static class ServiceMetadataProvider
{
    private static bool ServiceHasAttribute(Type service)
    {
        return service
            .CustomAttributes
            .Any(attribute => attribute.AttributeType == typeof(ServiceAttribute));
    }

    private static IEnumerable<Type> GetServicesFromAssembly(Assembly assembly)
    {
        return assembly.GetTypes().Where(ServiceHasAttribute);
    }

    public static IEnumerable<Type> GetAppServices()
    {
        var appServices = new List<Type>();

        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != null)
        {
            appServices.AddRange(GetServicesFromAssembly(entryAssembly));
        }

        var assembliesNames = entryAssembly?.GetReferencedAssemblies();

        if (assembliesNames == null) return appServices;

        foreach (var assemblyName in assembliesNames)
        {
            var referenceAssembly = Assembly.Load(assemblyName);
            if (referenceAssembly.GetCustomAttribute<AssemblyCompanyAttribute>()!.Company
                .Contains("Microsoft")) continue;

            var referencedAssemblyServices = GetServicesFromAssembly(referenceAssembly);
            appServices.AddRange(referencedAssemblyServices);
        }

        return appServices;
    }

    public static Type? GetDefaultInterfaceOfService(Type service)
    {
        return service
            .GetInterfaces()
            .FirstOrDefault(inter => inter.Name.EndsWith(service.Name));
    }

    public static ServiceLifetime? GetServiceLifetime(Type service)
    {
        var attribute = service
            .CustomAttributes
            .FirstOrDefault(attr => attr.AttributeType == typeof(ServiceAttribute));

        if (attribute == null) return null;

        var serviceLifeTime = attribute
            .ConstructorArguments
            .FirstOrDefault(arg => arg.ArgumentType == typeof(ServiceLifetime))
            .Value;

        return (ServiceLifetime)serviceLifeTime!;
    }
}