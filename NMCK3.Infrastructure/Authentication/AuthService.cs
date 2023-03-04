using NMCK3.Application.Abstractions.Authentication;
using NMCK3.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Infrastructure.Authentication
{
    public class AuthService : IAuthService
    {

        private readonly IJwtTokenProvider _jwtTokenProvider;
        private readonly IRegisterService _registerService;
        public AuthService(IJwtTokenProvider jwtTokenProvider, IRegisterService registerService)
        {
            _jwtTokenProvider = jwtTokenProvider;
            _registerService = registerService;
        }

        public async Task<string> Register(string firstName, string lastName, Email email, string password, CancellationToken cancellationToken = default)
        {
            var registerResult = await _registerService.Register(
                firstName, lastName, email.Value, password, cancellationToken);

            if (registerResult.IsFailure)
                return null;

            return await _jwtTokenProvider.Generate(email.Value, password, cancellationToken);
        }

        public async Task<string> Login(Email email, string password, CancellationToken cancellationToken = default)
        {
            return await _jwtTokenProvider.Generate(email.Value, password, cancellationToken);
        }
    }
}
