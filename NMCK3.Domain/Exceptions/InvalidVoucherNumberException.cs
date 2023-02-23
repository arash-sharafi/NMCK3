using NMCK3.Shared.Exceptions;

namespace NMCK3.Domain.Exceptions
{
    public class InvalidVoucherNumberException:Mock3Exception
    {
        public InvalidVoucherNumberException() : base("Voucher number is not valid.")
        {
        }
    }
}
