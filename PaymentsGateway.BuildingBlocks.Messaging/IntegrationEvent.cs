namespace PaymentsGateway.BuildingBlocks.Messaging;

public abstract record IntegrationEvent(string Id) : IEvent;