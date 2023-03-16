using NMCK3.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace NMCK3.Domain.Entities
{
    public class PersianDate : ValueObject
    {
        public PersianDate(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public DateTime AddMonths(int months)
        {
            return ((DateTime)new PersianDate(Value)).AddMonths(months);
        }

        public static implicit operator DateTime(PersianDate date)
        {

            var year = int.Parse(date.Value.Substring(0, 4));
            var month = int.Parse(date.Value.Substring(4, 2));
            var day = int.Parse(date.Value.Substring(6, 2));

            return new DateTime(year, month, day, new PersianCalendar());
        }

        public static implicit operator PersianDate(DateTime date)
        {
            return GetDate(date);
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
            if (left == null || right == null)
                return false;

            return (DateTime)left > (DateTime)right;
        }

        public static bool operator <=(PersianDate left, PersianDate right)
        {
            if (left == null || right == null)
                return false;

            return (DateTime)left <= (DateTime)right;
        }

        public static bool operator >=(PersianDate left, PersianDate right)
        {
            if (left == null || right == null)
                return false;

            return (DateTime)left >= (DateTime)right;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private static PersianDate GetDate(DateTime dateTime)
        {
            var persianCalendar = new PersianCalendar();
            var year = persianCalendar.GetYear(dateTime.Date);

            var month = persianCalendar.GetMonth(dateTime.Date) > 9
                ? persianCalendar.GetMonth(dateTime.Date).ToString()
                : "0" + persianCalendar.GetMonth(dateTime.Date);

            var day = persianCalendar.GetDayOfMonth(dateTime.Date) > 9
                ? persianCalendar.GetDayOfMonth(dateTime.Date).ToString()
                : "0" + persianCalendar.GetDayOfMonth(dateTime.Date);

            return new PersianDate(year + month + day);
        }

        public static PersianDate Today(DateTime now) => GetDate(now);
    }
}
