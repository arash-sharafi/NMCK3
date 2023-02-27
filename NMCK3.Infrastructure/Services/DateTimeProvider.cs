using NMCK3.Application.Common.Services;
using System;

namespace NMCK3.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
