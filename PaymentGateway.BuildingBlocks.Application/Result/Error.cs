namespace PaymentGateway.BuildingBlocks.Application.Result;

public record Error(string PropertyName, string ErrorMessage);
public record ErrorNotFound(string PropertyName) : Error(PropertyName, "NotFound");