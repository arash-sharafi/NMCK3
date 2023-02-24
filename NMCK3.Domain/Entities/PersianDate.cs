using NMCK3.Domain.Primitives;
using System;
using System.Collections.Generic;

namespace NMCK3.Domain.Entities
{
    public class PersianDate : ValueObject
    {
        public PersianDate(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static implicit operator DateTime(PersianDate date)
        {
            var year = int.Parse(date.Value.Substring(0, 4));
            var month = int.Parse(date.Value.Substring(4, 2));
            var day = int.Parse(date.Value.Substring(6, 2));

            return new DateTime(year, month, day);
        }

        public static implicit operator string(PersianDate date)
        {
            return date.Value;
        }

        public static bool operator <(PersianDate left, PersianDate right)
        {
            if (left == null || right == null)
                return false;

            return (DateTime)left < (DateTime)right;
        }

        public static bool operator >(PersianDate left, PersianDate right)
        {
            return !(left < right);
        }

        public static bool operator <=(PersianDate left, PersianDate right)
        {
            if (left == null || right == null)
                return false;

            return (DateTime)left <= (DateTime)right;
        }

        public static bool operator >=(PersianDate left, PersianDate right)
        {
            return !(left <= right);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
