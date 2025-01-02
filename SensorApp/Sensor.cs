using System;
using ReactiveUI;

namespace SensorSimulation
{
    public class Sensor : ReactiveObject
    {
        private int _currentValue;
        public int CurrentValue
        {
            get => _currentValue;
            private set => this.RaiseAndSetIfChanged(ref _currentValue, value);
        }

        public void UpdateValue(int value)
        {
            CurrentValue = Math.Clamp(value, -100, 100); // Clamp value between -100 and 100
        }
    }
}
