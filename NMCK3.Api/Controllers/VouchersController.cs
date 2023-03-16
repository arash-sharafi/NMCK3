using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NMCK3.Application.Vouchers.Commands.Buy;
using NMCK3.Shared.Contracts.Vouchers;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]")]
    public class VouchersController : ApiController
    {
        public VouchersController(ISender sender, IHttpContextAccessor httpContextAccessor)
            : base(sender, httpContextAccessor)
        {
        }

        [HttpPost("buy")]
        public async Task<IActionResult> Buy(BuyVoucherRequest request, CancellationToken cancellationToken)
        {
            var command = new BuyVoucherCommand(
                UserId,
                request.Count);

            var result = await Sender.Send(command, cancellationToken);

            return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
        }
    }
}
