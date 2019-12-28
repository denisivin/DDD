using System;
using System.Collections.Generic;

namespace ValueObjects
{
    public class DataSpeed : ValueObject<DataSpeed>
    {
        public DataSpeed() : this(0m) { }

        public DataSpeed(decimal speedInMegabits)
        {
            Validate(speedInMegabits);

            if (speedInMegabits < 0)
                speedInMegabits = 0;

            SpeedInMegabits = speedInMegabits;
        }

        public decimal SpeedInMegabits { get; }

        public bool IsZero => SpeedInMegabits == default;

        public static DataSpeed FromValue(decimal value, SpeedMeasurementUnit measurement)
        {
            switch (measurement)
            {
                case SpeedMeasurementUnit.Kbps:
                    return new DataSpeed(Math.Round(value / 1024, 3, MidpointRounding.AwayFromZero));

                case SpeedMeasurementUnit.Mbps:
                    return new DataSpeed(value);

                case SpeedMeasurementUnit.Gbps:
                    return new DataSpeed(Math.Round(value * 1024, 3, MidpointRounding.AwayFromZero));

                default:
                    return new DataSpeed();
            }
        }

        public override string ToString() => SpeedInMegabits.ToString("#,0.000");

        private void Validate(decimal value)
        {
            if (value % 0.001m != 0)
                throw new MoreThanThreeDecimalPlacesInSpeedValueException();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SpeedInMegabits;
        }

        public static DataSpeed operator -(DataSpeed left, DataSpeed right) => new DataSpeed(left.SpeedInMegabits - right.SpeedInMegabits);
        public static DataSpeed operator +(DataSpeed left, DataSpeed right) => new DataSpeed(left.SpeedInMegabits + right.SpeedInMegabits);

        public enum SpeedMeasurementUnit
        {
            Kbps = 1,
            Mbps = 2,
            Gbps = 3,
        }
    }

    public class MoreThanThreeDecimalPlacesInSpeedValueException : ArgumentException
    {
    }
}