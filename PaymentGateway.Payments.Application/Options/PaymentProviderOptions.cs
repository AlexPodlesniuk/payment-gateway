namespace PaymentGateway.Payments.Application.Options;

public class PaymentProviderOptions
{
    public const string SectionName = "PaymentProvider";

    public string ProviderName { get; init; } = default!;
    public Uri ProviderEndpoint { get; init; } = default!;
}