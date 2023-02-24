using NMCK3.Domain.Common;
using NMCK3.Domain.Errors;
using NMCK3.Domain.Primitives;
using System.Collections.Generic;

namespace NMCK3.Domain.ValueObjects
{
    public class VoucherCode : ValueObject
    {
        public string Value { get; }

        private VoucherCode(string value)
        {
            Value = value;
        }

        public static Result<VoucherCode> Create(string voucherCode)
        {
            if (string.IsNullOrWhiteSpace(voucherCode))
                return Result.Fail<VoucherCode>(DomainErrors.VoucherCode.NullOrEmptyVoucherNumber);

            if (voucherCode.Trim().Length != 16)
                return Result.Fail<VoucherCode>(DomainErrors.VoucherCode.InvalidLengthVoucherNumber);

            return new VoucherCode(voucherCode);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
