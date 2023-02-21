namespace PaymentGateway.BuildingBlocks.Application.Result;

public interface IValidationResult
{
    public static Error ValidationError => new("ValidationError", "Validation problem occured");
    Error[] Errors { get; }
}