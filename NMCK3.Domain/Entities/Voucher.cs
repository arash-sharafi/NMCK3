using NMCK3.Domain.Exceptions;
using System;

namespace NMCK3.Domain.Entities
{
    public class Voucher
    {
        private Voucher(Guid id, string voucherNo, string createDate)
        {
            Id = id;
            VoucherNo = voucherNo;
            CreateDate = createDate;
        }

        public Guid Id { get; private set; }

        public string VoucherNo { get; private set; }

        public string CreateDate { get; private set; }

        public static Voucher Create(string voucherNo)
        {
            if (string.IsNullOrWhiteSpace(voucherNo))
                throw new InvalidVoucherNumberException();

            string today = "TodayDateInPersian";

            var voucher = new Voucher(Guid.NewGuid(), voucherNo, today);

            return voucher;
        }
    }
}
