using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NMCK3.Application.Exams.Commands.Create;
using NMCK3.Shared.Contracts.Exams;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [Route("[controller]")]
    public class ExamsController : ApiController
    {
        public ExamsController(ISender sender) : base(sender)
        {
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

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return Ok(result.Value);
        }


    }
}
