# AOD.packages.AttributeInjector

AOD.packages.AttributeInjector is a lightweight package for automatic registration of services with dependency injection in .NET applications.

## Installation

To install AOD.packages.AttributeInjector, you can use NuGet Package Manager or the .NET CLI:


## Usage

1. **Register Attribute Injector**: In your `Program.cs` file, add the Attribute Injector to the service collection by calling `AddAttributeInjector()` method:

    ```csharp
    builder.Services.AddAttributeInjector();
    ```

2. **Register Services**: Use the `[Service]` attribute to register your services with the desired lifetime:

    ```csharp
    [Service(Lifetime.Singleton)]
    public class MyService
    {
        // Implementation
    }
    ```

    This is equivalent to:

    ```csharp
    services.AddSingleton<MyService>();
    ```

3. **Automatic Interface Detection**: The package automatically detects interfaces implemented by your services and registers them as the service type. For example:

    ```csharp
    public interface IMyService
    {
        // Interface members
    }

    [Service(Lifetime.Singleton)]
    public class MyService : IMyService
    {
        // Implementation
    }
    ```

    This is equivalent to:

    ```csharp
    services.AddSingleton<IMyService, MyService>();
    ```

4. **Direct Registration**: If no interface is implemented by your service, it is registered directly with the container:

    ```csharp
    [Service(Lifetime.Transient)]
    public class AnotherService
    {
        // Implementation
    }
    ```

    This is equivalent to:

    ```csharp
    services.AddTransient<AnotherService>();
    ```

## Contribution

Contributions to AOD.packages.AttributeInjector are welcome! If you find any issues or have suggestions for improvement, please feel free to open an issue or create a pull request on the GitHub repository.

## License

This package is licensed under the [MIT License](LICENSE).
