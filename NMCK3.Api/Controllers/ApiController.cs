using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NMCK3.Domain.Shared;
using System;
using System.Security.Claims;

namespace NMCK3.Api.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected readonly ISender Sender;
        protected readonly string UserId;
        protected ApiController(ISender sender, IHttpContextAccessor httpContextAccessor)
        {
            Sender = sender;
            UserId = httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        protected IActionResult HandleFailure(Result result) =>
            result switch
            {
                { IsSuccess: true } => throw new InvalidOperationException(),
                IValidationResult validationResult =>
                    BadRequest(
                        CreateProblemDetails(
                            "Validation Error",
                            StatusCodes.Status400BadRequest,
                            result.Error,
                            validationResult.Errors)),
                _ =>
                    BadRequest(
                        CreateProblemDetails(
                            "Bad request",
                            StatusCodes.Status400BadRequest,
                            result.Error))
            };


        private static ProblemDetails CreateProblemDetails(
            string title,
            int status,
            Error error,
            Error[] errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
    }
}
