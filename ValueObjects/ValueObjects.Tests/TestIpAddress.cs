using System;
using Xunit;

namespace ValueObjects.Tests
{
    public class TestIpAddress
    {
        [Theory]
        [InlineData("0.0.0.0")]
        [InlineData("1.2.3.4")]
        [InlineData("255.255.255.255")]
        public void ShouldBeValid(string address)
        {
            var ipAddress = new IPAddress(address);

            Assert.True(ipAddress.Value == address);
        }

        [Theory]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("1.2.3")]
        [InlineData("2323.434.867.75476")]
        [InlineData("a.b.c.d")]
        [InlineData("some text")]
        public void ShouldThrowException(string address)
        {
            Assert.Throws<ValueObjects.NotValidIPAddressException>(() => new IPAddress(address));
        }
    }
}
