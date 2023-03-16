using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NMCK3.Application.ExamReservations.Commands.Add;
using NMCK3.Application.Exams.Commands.Create;
using NMCK3.Shared.Contracts.ExamReservations;
using NMCK3.Shared.Contracts.Exams;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]")]
    public class ExamsController : ApiController
    {
        public ExamsController(ISender sender, IHttpContextAccessor httpContextAccessor)
            : base(sender, httpContextAccessor)
        {
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateExamRequest request,
                CancellationToken cancellationToken)
        {
            var command = new CreateExamCommand(
                request.Name,
                request.ExamDate,
                request.Description,
                request.Capacity);

            var result = await Sender.Send(command, cancellationToken);

            return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
        }

        [HttpPost("addReservation")]
        public async Task<IActionResult> AddExamReservation(AddExamReservationRequest request,
            CancellationToken cancellationToken)
        {
            var command = new AddExamReservationCommand(
                ParticipantId: UserId,
                request.ExamId,
                request.VoucherId);

            var result = await Sender.Send(command, cancellationToken);

            return result.IsFailure ? HandleFailure(result) : Ok();
        }
    }
}
