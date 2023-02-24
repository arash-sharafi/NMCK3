using NMCK3.Domain.Entities;
using System;
using System.Globalization;

namespace NMCK3.Domain.Common
{
    public static class Utilities
    {
        public static PersianDate TodayDate()
        {
            var persianCalendar = new PersianCalendar();
            var year = persianCalendar.GetYear(DateTime.Now);
            var month = persianCalendar.GetMonth(DateTime.Now);
            var day = persianCalendar.GetDayOfMonth(DateTime.Now);

            return new PersianDate(year + month.ToString() + day);
        }
    }
}
