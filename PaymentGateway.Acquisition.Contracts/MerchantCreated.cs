using PaymentsGateway.BuildingBlocks.Messaging;

namespace PaymentGateway.Acquisition.Contracts;
public record MerchantCreated(string Id, string MerchantName, string BankAccount) : IntegrationEvent(Id);