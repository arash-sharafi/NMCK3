using NMCK3.Domain.Common;
using NMCK3.Domain.Entities;
using NMCK3.Domain.Errors;
using System;

namespace NMCK3.Domain.ValueObjects
{
    public class VoucherPurchaseDate : PersianDate
    {
        public VoucherPurchaseDate(string value) : base(value)
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