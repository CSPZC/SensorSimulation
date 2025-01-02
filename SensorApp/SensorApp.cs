using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace SensorSimulation
{
    public class SensorApp : Application
    {
        public override void Initialize()
        {
            Styles.Add(new Avalonia.Themes.Fluent.FluentTheme(null));
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var sensor = new Sensor();
                var mainWindow = new MainWindow(sensor);
                desktop.MainWindow = mainWindow;

                // Run console input handling in a background task
                Task.Run(() => HandleConsoleInput(sensor));
            }
            base.OnFrameworkInitializationCompleted();
        }

        private void HandleConsoleInput(Sensor sensor)
        {
            while (true)
            {
                Console.Write("Enter a sensor value (-100 to 100): ");
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    sensor.UpdateValue(value);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an integer value.");
                }
            }
        }
    }
}
