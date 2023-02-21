using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.BuildingBlocks.Application.Result;

namespace PaymentGateway.BuildingBlocks.Api.Abstractions;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;
    protected ApiController(ISender sender) => Sender = sender;
    protected IActionResult HandleFailure(Result result) => result switch
    {
        { IsSuccess: true } => throw new InvalidOperationException(),
        { Error: ErrorNotFound } => NotFound(),
        IValidationResult validationResult =>
            BadRequest(CreateProblemDetails(
                "Validation error",
                StatusCodes.Status400BadRequest,
                result.Error!,
                validationResult.Errors)),
        _ => BadRequest(CreateProblemDetails(
            "Validation error",
            StatusCodes.Status400BadRequest,
            result.Error!,
            new[] { result.Error }!))
    };

    private static ProblemDetails CreateProblemDetails(string title, int status, Error error, Error[]? errors = null)
        => new()
        {
            Title = title,
            Detail = error.ErrorMessage,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}