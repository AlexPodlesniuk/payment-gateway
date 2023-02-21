using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PaymentGateway.Payments.Application.Queries.GetMerchantById;

namespace PaymentGateway.Payments.Api.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : Attribute, IAsyncAuthorizationFilter
{
    private const string ApiKey = "X-Api-Key";

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var apiKey = context.HttpContext.Request.Headers[ApiKey];

        if (string.IsNullOrEmpty(apiKey))
        {
            context.Result = new BadRequestResult();
            return;
        }

        var sender = context.HttpContext.RequestServices.GetRequiredService<ISender>();
        var verificationResult = await sender.Send(new GetMerchantById(apiKey!));
        if (verificationResult.IsFailure)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}