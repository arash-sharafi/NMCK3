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

        public static class ExamReservation
        {
            public static readonly Error NullParticipant = new Error(
                "ExamReservation.NullParticipant",
                "Participant can not be null.");

            
        }

        public static class Participant
        {
            public static readonly Error NullOrEmptyEmail = new Error(
                "Participant.NullOrEmptyEmail",
                "Participant Email Address can not be null.");
        }

        public static class Voucher
        {
            public static readonly Error NullOrEmptyVoucherNumber = new Error(
                "Voucher.NullOrEmpty",
                "Voucher Number can not be null or empty.");

            public static readonly Error InvalidLengthVoucherNumber = new Error(
                "Voucher.InvalidLengthVoucherNumber",
                "Voucher Number cannot be less than 16 digits.");
        }
    }
}
