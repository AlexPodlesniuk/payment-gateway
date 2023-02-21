using PaymentGateway.BuildingBlocks.Domain;

namespace PaymentsGateway.BuildingBlocks.Messaging;

public class NServiceBusEventPublisher : IIntegrationEventPublisher
{
    private readonly IMessageSession _messageSession;

    public NServiceBusEventPublisher(IMessageSession messageSession)
    {
        _messageSession = messageSession;
    }

    public async Task PublishAsync(IntegrationEvent @event, CancellationToken cancellationToken = default)
    {
        await _messageSession.Publish(@event, cancellationToken);
    }
}