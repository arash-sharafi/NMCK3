using NMCK3.Domain.Common;
using NMCK3.Domain.Errors;
using NMCK3.Domain.Primitives;
using System;

namespace NMCK3.Domain.Entities
{
    public sealed class Voucher : Entity
    {
        private Voucher(Guid id, string voucherNo, string createDate)
            : base(id)
        {
            VoucherNo = voucherNo;
            CreateDate = createDate;
        }


        public string VoucherNo { get; private set; }

        public string CreateDate { get; private set; }

        public static Result<Voucher> Create(string voucherNo)
        {
            if (string.IsNullOrWhiteSpace(voucherNo))
                return Result.Fail<Voucher>(DomainErrors.Voucher.NullOrEmptyVoucherNumber);

            if (voucherNo.Trim().Length != 16)
                return Result.Fail<Voucher>(DomainErrors.Voucher.InvalidLengthVoucherNumber);


            string today = "TodayDateInPersian";

            var voucher = new Voucher(Guid.NewGuid(), voucherNo, today);

            return voucher;
        }
    }
}
