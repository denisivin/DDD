using System;
using System.Collections.Generic;
using System.Linq;

namespace ValueObjects
{
    public class IPAddress : ValueObject<IPAddress>
    {
        public IPAddress() : this(string.Empty) { }

        public IPAddress(string ipAddress)
        {
            Validate(ipAddress);

            Value = ipAddress;
        }

        public string Value { get; }

        public override string ToString() => $"{Value}";

        private void Validate(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress))
                throw new NotValidIPAddressException();

            string[] splitValues = ipAddress.Split('.');
            if (splitValues.Length != 4)
                throw new NotValidIPAddressException();

            byte tempForParsing;

            if (!splitValues.All(r => byte.TryParse(r, out tempForParsing)))
                throw new NotValidIPAddressException();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }

    public class NotValidIPAddressException : ArgumentException
    {
    }
}