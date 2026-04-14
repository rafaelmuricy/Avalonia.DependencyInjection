# Avalonia.DependencyInjection

## Usage

In your prefered platform project, you create your service that implements an Interface, and register the service in the entry point of your applications.

### Android:

**MainActivity.cs**
```csharp
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .AddSingleton<IImageService, ImageService>()
            .WithInterFont();
    }
}
```

### iOS:

**AppDelegate.cs**
```csharp
public partial class AppDelegate : AvaloniaAppDelegate<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .AddSingleton<IImageService, ImageService>()
            .WithInterFont();
    }
}
```

### Desktop:

**Program.cs**
```csharp
sealed class Program
{
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .AddSingleton<IImageService, ImageService>()
            .LogToTrace();
}
```