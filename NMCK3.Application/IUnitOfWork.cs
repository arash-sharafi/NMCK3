using NMCK3.Application.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Application
{
    public interface IUnitOfWork
    {
        public IExamRepository Exams { get; }
        public IVoucherRepository Vouchers { get; }
        public IUserRepository Users { get; }
        Task CompleteAsync(CancellationToken cancellationToken = default);
    }
}
