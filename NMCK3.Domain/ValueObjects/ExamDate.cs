using NMCK3.Domain.Entities;
using NMCK3.Domain.Errors;
using NMCK3.Domain.Shared;
using System;

namespace NMCK3.Domain.ValueObjects
{
    public class ExamDate : PersianDate
    {
        private ExamDate(string value) : base(value)
        {
        }

        public static Result<ExamDate> Create(string examDate, DateTime currentDate)
        {
            if (string.IsNullOrWhiteSpace(examDate))
                return Result.Fail<ExamDate>(DomainErrors.ExamDate.NullOrEmptyExamDate);

            if (Today(currentDate) > new PersianDate(examDate))
                return Result.Fail<ExamDate>(DomainErrors.ExamDate.InvalidExamDate);

            return new ExamDate(examDate);
        }
    }
}
