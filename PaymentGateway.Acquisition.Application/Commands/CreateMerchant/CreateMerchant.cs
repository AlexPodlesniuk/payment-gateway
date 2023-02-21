using PaymentGateway.BuildingBlocks.Application.Mediatr;

namespace PaymentGateway.Acquisition.Application.Commands.CreateMerchant;

public record CreateMerchant(string Name, string BankAccount) : ICommand<MerchantCreationResult>;
public record MerchantCreationResult(string MerchantId);