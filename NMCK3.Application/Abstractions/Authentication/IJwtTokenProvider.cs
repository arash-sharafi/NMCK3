using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Application.Abstractions.Authentication
{
    public interface IJwtTokenProvider
    {
        Task<string> Generate(string email, string password, CancellationToken cancellationToken = default);
    }
}
