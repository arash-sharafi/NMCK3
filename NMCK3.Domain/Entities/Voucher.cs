using NMCK3.Domain.Primitives;
using NMCK3.Domain.Shared;
using NMCK3.Domain.ValueObjects;
using System;

namespace NMCK3.Domain.Entities
{
    public sealed class Voucher : AggregateRoot
    {
        private Voucher(Guid id, VoucherCode voucherCode, VoucherPurchaseDate purchaseDate, User buyer)
            : base(id)
        {
            VoucherCode = voucherCode;
            PurchaseDate = purchaseDate;
            Buyer = buyer;
        }


        public VoucherCode VoucherCode { get; private set; }

        public VoucherPurchaseDate PurchaseDate { get; private set; }
        public User Buyer { get; private set; }

        public static Result<Voucher> Create(VoucherCode voucherCode, User buyer, VoucherPurchaseDate purchaseDate, DateTime now)
        {
            var voucher = new Voucher(Guid.NewGuid(), voucherCode, purchaseDate, buyer);

            return voucher;
        }

        private Voucher()
        {

        }
    }
}
