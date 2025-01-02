using Avalonia;

namespace SensorSimulation
{
    public class Program
    {
        public static void Main(string[] args) => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<SensorApp>()
                         .UsePlatformDetect()
                         .LogToTrace();
    }
}
