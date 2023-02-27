using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMCK3.Application.Common.Services;
using NMCK3.Domain.Entities;

namespace NMCK3.Application.Common
{
    public static class Utilities
    {
        public const int VoucherValidationInMonths = 6;
        

        public static DateTime GetVoucherExpirationDate(Voucher voucher, 
            int validationInMonths)
        {
            return voucher.PurchaseDate.AddMonths(validationInMonths);

        }
    }
}
