using MediatR;
using PaymentGateway.BuildingBlocks.Application.Result;

namespace PaymentGateway.BuildingBlocks.Application.Mediatr;

public interface IQuery<TResult> : IRequest<Result<TResult>>
{
}