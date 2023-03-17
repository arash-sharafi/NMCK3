using NMCK3.Domain.Shared;

namespace NMCK3.Application.Common.Errors
{
    public static class ApplicationErrors
    {
        public static class Voucher
        {
            public static readonly Error InvalidReservationAttempt = new Error(
                "Voucher.InvalidReservationAttempt",
                "Invalid Attempt to a reservation.");
        }

        public static class User
        {
            public static readonly Error InvalidCredentials = new Error(
                "User.InvalidCredentials",
                "Invalid credentials");
        }
    }
}
