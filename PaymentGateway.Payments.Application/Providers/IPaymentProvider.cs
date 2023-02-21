using PaymentGateway.BuildingBlocks.Application.Result;

namespace PaymentGateway.Payments.Application.Providers;

public interface IPaymentProvider
{
    Task<Result<string>> ProcessPaymentAsync(PaymentInfo paymentInfo,CancellationToken cancellationToken = default);
}