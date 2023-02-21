using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Acquisition.Api.Models;
using PaymentGateway.Acquisition.Application.Queries.GetMerchantById;
using PaymentGateway.BuildingBlocks.Api.Abstractions;

namespace PaymentGateway.Acquisition.Api.Controllers;

[Route("api/v1/merchants")]
public class MerchantsController : ApiController
{
    public MerchantsController(ISender sender) : base(sender)
    {
    }

    [HttpGet("{merchantId}")]
    [ProducesResponseType(typeof(MerchantDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPaymentDetails(string merchantId, CancellationToken cancellationToken)
    {
        var getMerchantResult = await Sender.Send(new GetMerchantById(merchantId), cancellationToken);

        if (getMerchantResult.IsFailure)
        {
            return HandleFailure(getMerchantResult);
        }
        
        return Ok(getMerchantResult.Value());
    }
    
    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMerchant([FromBody] CreateMerchantRequest request, CancellationToken cancellationToken)
    {
        var processingResult = await Sender.Send(request.ToCreateMerchantCommand(), cancellationToken);

        if (processingResult.IsFailure)
        {
            return HandleFailure(processingResult);
        }
        
        return Ok(processingResult.Value());
    }
}