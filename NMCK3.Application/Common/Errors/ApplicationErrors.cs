using NMCK3.Domain.Common;

namespace NMCK3.Application.Common.Errors
{
    public static class ApplicationErrors
    {
        public static class Voucher
        {
            public static readonly Error InvalidVoucher = new Error(
                "Voucher.InvalidVoucher",
                "The voucher code is not valid.");
        }
    }
}
