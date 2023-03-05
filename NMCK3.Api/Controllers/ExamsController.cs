using MediatR;
using Microsoft.AspNetCore.Mvc;
using NMCK3.Application.Exams.Commands.Create;
using NMCK3.Shared.Contracts.Exams;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Api.Controllers
{
    [Route("[controller]")]
    public class ExamsController : ApiController
    {
        private readonly ISender _mediator;

        public ExamsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateExamRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateExamCommand(
                request.Name,
                request.ExamDate,
                request.Description,
                request.Capacity);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return Ok(result.Value);
        }
    }
}
