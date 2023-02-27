using System;

namespace NMCK3.Application.Common.Services
{
    public interface IDateTimeProvider
    {
        public DateTime Now { get; }
    }
}
