using System.Threading;
using System.Threading.Tasks;
using NMCK3.Application;

namespace NMCK3.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public async Task CompleteAsync(CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
