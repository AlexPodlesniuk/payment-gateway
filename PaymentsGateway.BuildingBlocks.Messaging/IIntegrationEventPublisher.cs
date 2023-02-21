namespace PaymentsGateway.BuildingBlocks.Messaging;

public interface IIntegrationEventPublisher
{
    Task PublishAsync(IntegrationEvent @event, CancellationToken cancellationToken = default);
}