using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NMCK3.Application.Participants.Register;
using NMCK3.Shared.Contracts.Authentication;

namespace NMCK3.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {
        public AuthenticationController(ISender sender) : base(sender)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request,
            CancellationToken cancellationToken)
        {
            var command = new RegisterCommand(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.Password);

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return Ok(result.Value);
        }

    }
}
