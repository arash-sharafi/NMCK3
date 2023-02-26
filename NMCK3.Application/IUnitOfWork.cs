using System.Threading.Tasks;

namespace NMCK3.Application
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
