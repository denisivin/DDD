using System;
using Xunit;

namespace ValueObjects.Tests
{
    public class TestCoordinates
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(90, 180)]
        [InlineData(-90, -180)]
        public void ShouldBeValid(double lat, double lng)
        {
            var coordinates = new Coordinates(lat, lng);

            Assert.True(coordinates.Latitude == lat);
            Assert.True(coordinates.Longitude == lng);
        }

        [Theory]
        [InlineData(92, 0)]
        [InlineData(-100, 0)]
        public void ShouldThrowLatitudeException(double lat, double lng)
        {
            Assert.Throws<ValueObjects.LatitudeIsMoreThan90DegreesException>(() => new Coordinates(lat, lng));
        }

        [Theory]
        [InlineData(0, 186)]
        [InlineData(0, -200)]
        public void ShouldThrowLongitudeException(double lat, double lng)
        {
            Assert.Throws<ValueObjects.LongitudeIsMoreThan180DegreesException>(() => new Coordinates(lat, lng));
        }

        [Theory]
        [InlineData(38.898556, -77.037852, 38.897147, -77.043934, 549.63)]
        [InlineData(41.9631174, -87.6770458, 40.7628267, -73.9898293, 1149792.99)]
        public void DistanceCalculation(double lat, double lng, double targetLat, double targetLng, double expected)
        {
            var coordinates = new Coordinates(lat, lng);
            var distancesInMeters = coordinates.CalculateDistanceTo(new Coordinates(targetLat, targetLng));

            Assert.Equal(distancesInMeters, expected);
        }
    }
}
