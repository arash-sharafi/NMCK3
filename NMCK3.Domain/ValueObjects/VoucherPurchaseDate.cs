using NMCK3.Domain.Entities;
using NMCK3.Domain.Errors;
using NMCK3.Domain.Shared;
using System;

namespace NMCK3.Domain.ValueObjects
{
    public class VoucherPurchaseDate : PersianDate
    {
        private VoucherPurchaseDate(string value) : base(value)
        {
        }

        public static Result<VoucherPurchaseDate> Create(string purchaseDate, DateTime now)
        {
            if (new PersianDate(purchaseDate) != Today(now))
                return Result.Fail<VoucherPurchaseDate>(DomainErrors.VoucherPurchaseDate.InvalidPurchaseDate);

            return new VoucherPurchaseDate(purchaseDate);
        }

    }
}