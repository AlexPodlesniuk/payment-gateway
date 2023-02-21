using PaymentGateway.Acquisition.Application.Commands.CreateMerchant;

namespace PaymentGateway.Acquisition.Api.Models;

public record CreateMerchantRequest(string Name, string BankAccount)
{
    public CreateMerchant ToCreateMerchantCommand() => new CreateMerchant(Name, BankAccount);
}