using NMCK3.Application;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Infrastructure.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        public async Task CompleteAsync(CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
