using NMCK3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NMCK3.Application.Repositories
{
    public interface IVoucherRepository
    {
        Task<Voucher> GetVoucherByCode(string voucherCode);
        Task<Voucher> GetVoucherById(Guid voucherId);
        Task<IEnumerable<Voucher>> GetVouchersByUserId(Guid buyerId);
        void Add(Voucher voucher);
    }
}