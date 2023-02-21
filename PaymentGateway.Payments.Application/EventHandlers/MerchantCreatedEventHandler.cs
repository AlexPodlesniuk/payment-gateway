using Microsoft.Extensions.Logging;
using NServiceBus;
using PaymentGateway.Acquisition.Contracts;
using PaymentGateway.Payments.Domain.Merchants;

namespace PaymentGateway.Payments.Application.EventHandlers;

internal class MerchantCreatedEventHandler : IHandleMessages<MerchantCreated>
{
    private readonly IMerchantRepository _merchantRepository;
    private readonly ILogger<MerchantCreatedEventHandler> _logger;

    public MerchantCreatedEventHandler(IMerchantRepository merchantRepository, ILogger<MerchantCreatedEventHandler> logger)
    {
        _merchantRepository = merchantRepository;
        _logger = logger;
    }

    public async Task Handle(MerchantCreated message, IMessageHandlerContext context)
    {
        var merchant = new Merchant(message.Id, message.MerchantName, message.BankAccount);
        await _merchantRepository.SaveAsync(merchant, context.CancellationToken);
    }
}