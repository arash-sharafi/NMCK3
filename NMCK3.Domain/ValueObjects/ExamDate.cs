using NMCK3.Domain.Common;

namespace NMCK3.Domain.ValueObjects
{
    public class ExamDate : PersianDate
    {
        private ExamDate(string value) : base(value)
        {
        }

        public static Result<ExamDate> Create(string date)
        {
            if (Utilities.TodayDate() > new PersianDate(date))
                return Result.Fail<ExamDate>(new Error("ExamDate.Invalid",
                    "ExamDate can not be less than today date."));

            var examDate = new ExamDate(date);
            return examDate;
        }
    }
}
