using NMCK3.Shared.Exceptions;

namespace NMCK3.Domain.Exceptions
{
    public class EmptyExamNameException : Mock3Exception
    {
        public EmptyExamNameException() : base("Exam name cannot be empty")
        {
        }
    }
}

