namespace Avalonia.DependencyInjection;

public static class ServiceProvider
{
    static Dictionary<Type, Type> services = new();
    static Dictionary<Type, object> singletons = new();

    /// <summary>
    /// In Desktop and Browser projects, call this in Program.cs, preferably in the BuildAvaloniaApp() method.
    /// In iOS and Android projects, call this in the AppDelegate.cs file, preferably in the CustomizeAppBuilder() method.
    /// </summary>
    /// <typeparam name="TInterface"></typeparam>
    /// <typeparam name="TImplementation"></typeparam>
    /// <param name="builder"></param>
    public static Avalonia.AppBuilder AddScoped<TInterface, TImplementation>(this Avalonia.AppBuilder builder)
        where TImplementation : TInterface
    {
        services[typeof(TInterface)] = typeof(TImplementation);
        return builder;
    }

    /// <summary>
    /// In Desktop and Browser projects, call this in Program.cs, preferably in the BuildAvaloniaApp() method.
    /// In iOS and Android projects, call this in the AppDelegate.cs file, preferably in the CustomizeAppBuilder() method.
    /// </summary>
    /// <typeparam name="TInterface"></typeparam>
    /// <typeparam name="TImplementation"></typeparam>
    /// <param name="builder"></param>
    public static Avalonia.AppBuilder AddSingleton<TInterface, TImplementation>(this Avalonia.AppBuilder builder)
        where TImplementation : class, TInterface, new()
    {
        object instance = Activator.CreateInstance<TImplementation>()!;
        singletons[typeof(TInterface)] = instance;

        return builder;
    }

    public static TInterface GetService<TInterface>()
    {
        if (services.TryGetValue(typeof(TInterface), out var implementationType))
        {
            return (TInterface)Activator.CreateInstance(implementationType)!;
        }
        else if (singletons.TryGetValue(typeof(TInterface), out var singleton))
        {
            return (TInterface)singleton;
        }
        throw new InvalidOperationException($"Service of type {typeof(TInterface).FullName} is not registered.");
    }


}
