using NMCK3.Application.Abstractions.Authentication;
using NMCK3.Application.Abstractions.Messaging;
using NMCK3.Application.Common.Errors;
using NMCK3.Application.Repositories;
using NMCK3.Domain.Common;
using NMCK3.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Application.Participants.Login
{
    internal sealed class LoginQueryHandler : ICommandHandler<LoginQuery, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenProvider _jwtTokenProvider;
        private readonly IAuthService _authService;
        public LoginQueryHandler(IUserRepository userRepository, IJwtTokenProvider jwtTokenProvider, IAuthService authService)
        {
            _userRepository = userRepository;
            _jwtTokenProvider = jwtTokenProvider;
            _authService = authService;
        }

        public async Task<Result<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);

            if (email.IsFailure)
                return Result.Fail<string>(email.Error);

            var token = await _authService.Login(email.Value, request.Password, cancellationToken);


            if (string.IsNullOrEmpty(token))
                return Result.Fail<string>(ApplicationErrors.User.InvalidCredentials);

            return token;
        }
    }
}