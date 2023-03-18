using NMCK3.Domain.Shared;

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

            public static readonly Error NoAvailableReservationSpot = new Error(
                "ExamReservation.NoAvailableReservationSpot",
                "There is no available reservation spot for this exam.");
        }

        public static class ExamDate
        {
            public static readonly Error InvalidExamDate = new Error(
                "ExamDate.InvalidExamDate",
                "Exam date can not be less than today date.");

            public static readonly Error  NullOrEmptyExamDate = new Error(
                "ExamDate.NullOrEmptyExamDate",
                "Exam date can not be Null of Empty.");
        }

        public static class Email
        {
            public static readonly Error NullOrEmptyEmail = new Error(
                "Email.NullOrEmptyEmail",
                "Email address can not be null.");

            public static readonly Error InvalidEmail = new Error(
                "Email.InvalidEmail",
                "Email Address is not valid.");
        }

        public static class VoucherCode
        {
            public static readonly Error NullOrEmptyVoucherNumber = new Error(
                "VoucherCode.NullOrEmpty",
                "Voucher code can not be null or empty.");

            public static readonly Error InvalidLengthVoucherNumber = new Error(
                "VoucherCode.InvalidLengthVoucherNumber",
                "Voucher code has to be exactly 16 digits.");
        }

        public static class VoucherPurchaseDate
        {
            public static readonly Error InvalidPurchaseDate = new Error(
                "VoucherPurchaseDate.InvalidPurchaseDate",
                "Voucher purchase date is not valid.");
        }
    }
}
