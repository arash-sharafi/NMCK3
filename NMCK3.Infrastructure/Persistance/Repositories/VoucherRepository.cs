using NMCK3.Application.Repositories;
using NMCK3.Domain.Entities;
using NMCK3.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Infrastructure.Persistance.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly List<Voucher> _vouchers = new();
        public async Task<Voucher> GetVoucherByCode(VoucherCode voucherCode, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            return _vouchers.FirstOrDefault(x => x.VoucherCode == voucherCode);
        }

        public async Task<Voucher> GetVoucherById(Guid voucherId, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            return _vouchers.FirstOrDefault(x => x.Id == voucherId);
        }

        public async Task<IEnumerable<Voucher>> GetVouchersByUserId(Guid buyerId, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            return _vouchers.Where(x => x.Buyer.Id == buyerId).ToList();
        }

        public void Add(Voucher voucher)
        {
            _vouchers.Add(voucher);
        }
    }
}
