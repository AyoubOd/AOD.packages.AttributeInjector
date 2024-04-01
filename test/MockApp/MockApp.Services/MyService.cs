using AttributeInjector;
using AttributeInjector.Core;
using Microsoft.Extensions.DependencyInjection;

namespace MockApp.Services;

[Service(ServiceLifetime.Singleton)]
public class MyService : IMyService
{
    public void Print()
    {
        Console.WriteLine("Hello world from my service");
    }
}