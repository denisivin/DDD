using System.Collections.Generic;
using System.Linq;

namespace ValueObjects
{
    public abstract class ValueObject<T>
        where T : ValueObject<T>
    {
        public override bool Equals(object other) => Equals(other as T);

        public virtual bool Equals(T other)
        {
            if (other == null)
            {
                return false;
            }
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (var obj in this.GetEqualityComponents())
                hash = hash * 31 + ((obj == null) ? 0 : obj.GetHashCode());

            return hash;
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right) => !(left == right);
        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;

            return Equals(left, right);
        }
    }
}