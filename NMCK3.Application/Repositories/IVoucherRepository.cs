using NMCK3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NMCK3.Domain.ValueObjects;

namespace NMCK3.Application.Repositories
{
    public interface IVoucherRepository
    {
        Task<Voucher> GetVoucherByCode(VoucherCode voucherCode, CancellationToken cancellationToken = default);
        Task<Voucher> GetVoucherById(Guid voucherId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Voucher>> GetVouchersByUserId(Guid buyerId, CancellationToken cancellationToken = default);
        void Add(Voucher voucher);
    }
}