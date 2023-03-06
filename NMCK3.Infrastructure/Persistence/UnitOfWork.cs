using NMCK3.Application;
using NMCK3.Application.Repositories;
using NMCK3.Infrastructure.Persistence.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IExamRepository Exams { get; }
        public IVoucherRepository Vouchers { get; }
        public IUserRepository Users { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Exams = new ExamRepository(_context);
            Vouchers = new VoucherRepository(_context);
            Users = new UserRepository(_context);
        }

        public async Task CompleteAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
