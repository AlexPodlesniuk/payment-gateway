using PaymentGateway.BuildingBlocks.Domain;

namespace PaymentGateway.Acquisition.Domain.Merchant;

public interface IMerchantRepository : IRepository<Merchant>
{
    Task<Merchant?> FindByNameAndBankAccountAsync(string name, string account,
        CancellationToken cancellationToken = default);
}