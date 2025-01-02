using System;
using FsCheck;
using NUnit.Framework;

namespace SensorSimulation.Tests
{
    [TestFixture]
    public class SensorTests
    {
        [FsCheck.NUnit.Property]
        public Property sensorValueShouldMapToCorrectPositionTest()
        {
            return Prop.ForAll(
                Arb.From(Gen.Choose(-100, 100)),
                sensorValue =>
                {
                    // Map the sensor value to position
                    var position = MapSensorValueToPosition(sensorValue);

                    // Assert that the position is within the expected range
                    Assert.That(position, Is.InRange(50, 550));
                });
        }

        [Test]
        public void sensorValueBoundariesShouldMapCorrectlyTest()
        {
            // Test lower boundary
            Assert.AreEqual(50, MapSensorValueToPosition(-100));

            // Test upper boundary
            Assert.AreEqual(550, MapSensorValueToPosition(100));
        }

        [FsCheck.NUnit.Property]
        public Property sensorValueOutsideRangeShouldNotBreakTest()
        {
            return Prop.ForAll(
                Arb.From(Gen.OneOf(
                    Gen.Choose(int.MinValue, -101),
                    Gen.Choose(101, int.MaxValue))),
                sensorValue =>
                {
                    // Map the sensor value to position
                    var position = MapSensorValueToPosition(sensorValue);

                    // Assert that the position is clamped within the valid range
                    Assert.That(position, Is.InRange(50, 550));
                });
        }

        // Helper method to map sensor values to positions
        private static int MapSensorValueToPosition(int sensorValue)
        {
            // Clamp sensor value to the range [-100, 100]
            sensorValue = Math.Max(-100, Math.Min(100, sensorValue));

            // Map to X-axis range [50, 550]
            return 50 + (sensorValue + 100) * 5;
        }
    }
}
