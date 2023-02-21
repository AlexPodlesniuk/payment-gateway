using PaymentGateway.Acquisition.Contracts;
using PaymentGateway.Acquisition.Domain.Merchant;
using PaymentGateway.BuildingBlocks.Application.Mediatr;
using PaymentGateway.BuildingBlocks.Application.Result;
using PaymentsGateway.BuildingBlocks.Messaging;

namespace PaymentGateway.Acquisition.Application.Commands.CreateMerchant;

internal class CreateMerchantHandler : ICommandHandler<CreateMerchant, MerchantCreationResult>
{
    private readonly IMerchantRepository _merchantRepository;
    private readonly IIntegrationEventPublisher _eventPublisher;

    public CreateMerchantHandler(IMerchantRepository merchantRepository, IIntegrationEventPublisher eventPublisher)
    {
        _merchantRepository = merchantRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<Result<MerchantCreationResult>> Handle(CreateMerchant request,
        CancellationToken cancellationToken)
    {
        var merchant =
            await _merchantRepository.FindByNameAndBankAccountAsync(request.Name, request.BankAccount,
                cancellationToken);
        
        if (merchant is not null)
            return Result.Fail<MerchantCreationResult>(new Error("Merchant",
                "Merchant with the specified name and account already exists"));

        var merchantId = await _merchantRepository.AllocateIdentifierAsync(cancellationToken);
        var newMerchant = new Merchant(merchantId, request.Name, request.BankAccount);

        await _merchantRepository.SaveAsync(newMerchant, cancellationToken);

        await _eventPublisher.PublishAsync(
            new MerchantCreated(merchantId, newMerchant.Name, newMerchant.BankAccount), 
            cancellationToken);

        return Result.Ok(new MerchantCreationResult(merchantId));
    }
}