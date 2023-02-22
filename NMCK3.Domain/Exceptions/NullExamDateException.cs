using NMCK3.Shared.Exceptions;

namespace NMCK3.Domain.Exceptions
{
    public class NullExamDateException : Mock3Exception
    {
        public NullExamDateException() : base("Exam Date cannot be empty.")
        {
        }
    }
}