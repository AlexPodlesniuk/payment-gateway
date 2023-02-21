using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.BuildingBlocks.Api.Abstractions;
using PaymentGateway.Payments.Api.Authorization;
using PaymentGateway.Payments.Api.Models;
using PaymentGateway.Payments.Application.Queries.GetPaymentById;

namespace PaymentGateway.Payments.Api.Controllers;

[ApiKey]
[Route("api/v1/card/payments")]
public class CardPaymentsController : ApiController
{
    public CardPaymentsController(ISender sender) : base(sender)
    {
    }
    
    [HttpGet("{paymentId}")]
    [ProducesResponseType(typeof(PaymentDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPaymentDetails(string paymentId, CancellationToken cancellationToken)
    {
        var getPaymentResult = await Sender.Send(new GetPaymentById(paymentId), cancellationToken);

        if (getPaymentResult.IsFailure)
        {
            return NotFound(paymentId);
        }
        
        return Ok(getPaymentResult.Value());
    }
    
    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ProcessPayment([FromBody] CardPaymentRequest request, CancellationToken cancellationToken)
    {
        var processingResult = await Sender.Send(request.ToProcessPaymentCommand(), cancellationToken);

        if (processingResult.IsFailure)
        {
            return HandleFailure(processingResult);
        }
        
        return Created($"api/v1/card/payments/{processingResult.Value()!.PaymentId}", processingResult.Value());
    }
}