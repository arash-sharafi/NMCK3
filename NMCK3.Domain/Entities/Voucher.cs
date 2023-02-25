using NMCK3.Domain.Common;
using NMCK3.Domain.Primitives;
using NMCK3.Domain.ValueObjects;
using System;

namespace NMCK3.Domain.Entities
{
    public sealed class Voucher : AggregateRoot
    {
        private Voucher(Guid id, VoucherCode voucherCode, VoucherPurchaseDate purchaseDate)
            : base(id)
        {
            VoucherCode = voucherCode;
            PurchaseDate = purchaseDate;
        }


        public VoucherCode VoucherCode { get; private set; }

        public VoucherPurchaseDate PurchaseDate { get; private set; }

        public static Result<Voucher> Create(VoucherCode voucherCode)
        {
            var purchaseDate = VoucherPurchaseDate.Create(Utilities.TodayDate());

            var voucher = new Voucher(Guid.NewGuid(), voucherCode, purchaseDate.Value);

            return voucher;
        }
    }
}
