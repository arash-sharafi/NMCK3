using NMCK3.Application.Abstractions.Authentication;
using NMCK3.Application.Abstractions.Messaging;
using NMCK3.Application.Common.Errors;
using NMCK3.Domain.Shared;
using NMCK3.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Application.Participants.Register
{
    internal sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, string>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);

            if (email.IsFailure)
                return Result.Fail<string>(email.Error);

            var token = await _authService.Register(request.FirstName, request.LastName, email.Value, request.Password, cancellationToken);


            if (string.IsNullOrEmpty(token))
                return Result.Fail<string>(ApplicationErrors.User.InvalidCredentials);

            return token;
        }
    }
}
