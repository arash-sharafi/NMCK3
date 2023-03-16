using Microsoft.EntityFrameworkCore;
using NMCK3.Application.Repositories;
using NMCK3.Domain.Entities;
using NMCK3.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Infrastructure.Persistence.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly ApplicationDbContext _context;

        public VoucherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Voucher> GetVoucherByCode(VoucherCode voucherCode, CancellationToken cancellationToken = default)
        {
            return await _context.Vouchers
                .FirstOrDefaultAsync(x => x.VoucherCode == voucherCode, cancellationToken);
        }

        public async Task<Voucher> GetVoucherById(Guid voucherId, CancellationToken cancellationToken = default)
        {
            var voucher = await _context.Vouchers.FirstOrDefaultAsync(x => x.Id == voucherId, cancellationToken);
            return voucher;
        }

        public async Task<IEnumerable<Voucher>> GetVouchersByBuyerId(string buyerId, CancellationToken cancellationToken = default)
        {
            return await _context.Vouchers.Where(x => x.Buyer.Id == buyerId).ToListAsync(cancellationToken);
        }

        public void Add(Voucher voucher)
        {
            _context.Vouchers.Add(voucher);
        }
    }
}
