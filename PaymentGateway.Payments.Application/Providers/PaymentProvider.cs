using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using PaymentGateway.BuildingBlocks.Application.Result;
using PaymentGateway.Payments.Application.Options;

namespace PaymentGateway.Payments.Application.Providers;
public class PaymentProvider : IPaymentProvider
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<PaymentProviderOptions> _providerOptions;

    public PaymentProvider(HttpClient httpClient, IOptions<PaymentProviderOptions> providerOptions)
    {
        _httpClient = httpClient;
        _providerOptions = providerOptions;

        _httpClient.BaseAddress = _providerOptions.Value.ProviderEndpoint;
    }
    public async Task<Result<string>> ProcessPaymentAsync(PaymentInfo paymentInfo, CancellationToken cancellationToken = default)
    {
        try
        {
            var response =
                await _httpClient.PostAsJsonAsync("payments", paymentInfo, cancellationToken: cancellationToken);

            if (!TransferSuccessful(response))
                return Result.Fail<string>(new Error(_providerOptions.Value.ProviderName,
                    "Failed to process a transaction"));

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            return Result.Ok(responseContent);
        }
        catch (Exception)
        {
            return Result.Fail<string>(
                new Error(_providerOptions.Value.ProviderName, "Error during transaction execution"));
        }
    }

    public bool TransferSuccessful(HttpResponseMessage? response)
    {
        return response is not null && response.IsSuccessStatusCode;
    }
}