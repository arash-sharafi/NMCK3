using System.Threading;
using System.Threading.Tasks;
using NMCK3.Domain.Common;

namespace NMCK3.Infrastructure.Authentication
{
    public interface IRegisterService
    {
        Task<Result> Register(string firstName, string lastName, string email, string password, CancellationToken cancellationToken = default);
    }
}