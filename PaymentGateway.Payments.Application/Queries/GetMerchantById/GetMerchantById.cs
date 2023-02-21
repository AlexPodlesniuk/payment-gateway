using PaymentGateway.BuildingBlocks.Application.Mediatr;

namespace PaymentGateway.Payments.Application.Queries.GetMerchantById;

public record GetMerchantById(string MerchantId) : IQuery<MerchantDetails?>;
public record MerchantDetails(string Name);