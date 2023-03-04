using NMCK3.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Application.Abstractions.Authentication
{
    public interface IAuthService
    {
        Task<string> Register(string firstName, string lastName, Email email, string password
        , CancellationToken cancellationToken = default);
        Task<string> Login(Email email, string password, CancellationToken cancellationToken = default);
    }
}
