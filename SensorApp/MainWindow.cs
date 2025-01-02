using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Threading;
using ReactiveUI;

namespace SensorSimulation
{
    public class MainWindow : Window
    {
        private readonly Sensor _sensor;
        private readonly Line _arrowLine;
        private readonly TextBlock _valueTextBlock;

        public MainWindow(Sensor sensor)
        {
            _sensor = sensor;

            Title = "Sensor Simulation with Avalonia UI and Console Input";
            Width = 600;
            Height = 300;

            var canvas = new Canvas
            {
                Background = Brushes.White,
                Width = 600,
                Height = 150
            };

            // Add colored regions
            var greenRegion = new Rectangle
            {
                Fill = Brushes.LightGreen,
                Width = 166,
                Height = 20,
                Margin = new Thickness(50, 55, 0, 0)
            };
            canvas.Children.Add(greenRegion);

            var yellowRegion = new Rectangle
            {
                Fill = Brushes.LightYellow,
                Width = 166,
                Height = 20,
                Margin = new Thickness(216, 55, 0, 0)
            };
            canvas.Children.Add(yellowRegion);

            var redRegion = new Rectangle
            {
                Fill = Brushes.LightCoral,
                Width = 166,
                Height = 20,
                Margin = new Thickness(382, 55, 0, 0)
            };
            canvas.Children.Add(redRegion);

            // Draw axis
            var axis = new Line
            {
                StartPoint = new Avalonia.Point(50, 75),
                EndPoint = new Avalonia.Point(550, 75),
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            canvas.Children.Add(axis);

            // Add vertical ticks and labels
            AddTick(canvas, 50, "-100");
            AddTick(canvas, 300, "0");
            AddTick(canvas, 550, "100");

            // Arrow line
            _arrowLine = new Line
            {
                StartPoint = new Avalonia.Point(300, 75),
                EndPoint = new Avalonia.Point(300, 55),
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };
            canvas.Children.Add(_arrowLine);

            _valueTextBlock = new TextBlock
            {
                Text = "Value: 0",
                FontSize = 16,
                Margin = new Thickness(10)
            };

            var stackPanel = new StackPanel
            {
                Children =
                {
                    _valueTextBlock,
                    canvas
                }
            };

            Content = stackPanel;

            // Reactive updates for sensor value
            sensor.WhenAnyValue(x => x.CurrentValue)
                  .Subscribe(value =>
                  {
                      // Update UI
                      Dispatcher.UIThread.InvokeAsync(() =>
                      {
                          _valueTextBlock.Text = $"Value: {value}";
                          var position = 50 + (value + 100) * 500 / 200; // Map sensor value to position
                          _arrowLine.StartPoint = new Avalonia.Point(position, 75);
                          _arrowLine.EndPoint = new Avalonia.Point(position, 55);
                      });
                  });
        }

        private void AddTick(Canvas canvas, double position, string label)
        {
            // Vertical tick
            var tick = new Line
            {
                StartPoint = new Avalonia.Point(position, 75),
                EndPoint = new Avalonia.Point(position, 85),
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            canvas.Children.Add(tick);

            // Label
            var tickLabel = new TextBlock
            {
                Text = label,
                FontSize = 14,
                Foreground = Brushes.Black,
                Margin = new Thickness(position - 15, 90, 0, 0)
            };
            canvas.Children.Add(tickLabel);
        }
    }
}
