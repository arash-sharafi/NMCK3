using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NMCK3.Domain.Shared;
using System;

namespace NMCK3.Api.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected IActionResult HandleFailure(Result result)=>
            result switch
            {
                { IsSuccess: true } => throw new InvalidOperationException(),
                IValidationResult validationResult=>
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
