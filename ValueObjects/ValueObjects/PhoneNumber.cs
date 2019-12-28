using System.Collections.Generic;

namespace ValueObjects
{
    public class PhoneNumber : ValueObject<PhoneNumber>
    {
        public PhoneNumber() { }
        public PhoneNumber(string number) => Number = number;

        public string Number { get; }

        protected override IEnumerable<object> GetEqualityComponents() { yield return Number; }
    }
}