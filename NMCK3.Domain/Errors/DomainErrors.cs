using NMCK3.Domain.Common;

namespace NMCK3.Domain.Errors
{
    public static class DomainErrors
    {
        public static class Exam
        {
            public static readonly Error EmptyName = new Error(
                "Exam.EmptyName",
                "Exam name cannot be empty.");
        }
    }
}
