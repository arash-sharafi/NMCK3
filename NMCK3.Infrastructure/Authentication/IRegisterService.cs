using NMCK3.Domain.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Infrastructure.Authentication
{
    public interface IRegisterService
    {
        Task<Result> Register(string firstName, string lastName, string email, string password, CancellationToken cancellationToken = default);
    }
}