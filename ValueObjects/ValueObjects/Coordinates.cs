using System;
using System.Collections.Generic;

namespace ValueObjects
{
    public class Coordinates : ValueObject<Coordinates>
    {
        public Coordinates() : this(0, 0) { }

        public Coordinates(double? latitude, double? longitude)
        {
            Validate(latitude ?? 0, longitude ?? 0);

            Latitude = latitude;
            Longitude = longitude;
        }

        public double? Latitude { get; }
        public double? Longitude { get; }

        public bool IsEquator => Latitude == default;

        public bool IsPrimeMeridian => Longitude == default;

        public bool IsZero => IsEquator && IsPrimeMeridian;

        /// <summary>
        /// Returns the distance between the latitude and longitude coordinates  that are specified by this Coordinates
        /// and another specified Coordinates.
        /// </summary>
        /// <param name="targetPoint">The Coordinates for the location to calculate the distance to.</param>
        /// <returns>The distance between the two coordinates, in meters.</returns>
        public double CalculateDistanceTo(Coordinates targetPoint)
        {
            var d1 = (Latitude ?? 0) * (Math.PI / 180.0);
            var num1 = (Longitude ?? 0) * (Math.PI / 180.0);
            var d2 = (targetPoint.Latitude ?? 0) * (Math.PI / 180.0);
            var num2 = (targetPoint.Longitude ?? 0) * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return Math.Round(6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3))), 2);
        }

        public override string ToString() => $"{Latitude:#.00}, {Longitude:#.00}";

        private void Validate(double latitude, double longitude)
        {
            if (Math.Abs(latitude) > 90)
                throw new LatitudeIsMoreThan90DegreesException();
            if (Math.Abs(longitude) > 180)
                throw new LongitudeIsMoreThan180DegreesException();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }

    public class LatitudeIsMoreThan90DegreesException : ArgumentException
    {
    }

    public class LongitudeIsMoreThan180DegreesException : ArgumentException
    {
    }
}