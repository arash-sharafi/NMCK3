using NMCK3.Domain.Common;
using NMCK3.Domain.Entities;
using NMCK3.Domain.Errors;

namespace NMCK3.Domain.ValueObjects
{
    public class ExamDate : PersianDate
    {
        private ExamDate(string value) : base(value)
        {
        }

        public static Result<ExamDate> Create(string examDate)
        {
            if (string.IsNullOrWhiteSpace(examDate))
                return Result.Fail<ExamDate>(DomainErrors.ExamDate.NullOrEmptyExamDate);

            if (Utilities.TodayDate() > new PersianDate(examDate))
                return Result.Fail<ExamDate>(DomainErrors.ExamDate.InvalidExamDate);

            return new ExamDate(examDate);
        }
    }
}
