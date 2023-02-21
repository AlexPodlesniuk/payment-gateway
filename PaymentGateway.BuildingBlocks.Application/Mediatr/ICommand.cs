using MediatR;
using PaymentGateway.BuildingBlocks.Application.Result;

namespace PaymentGateway.BuildingBlocks.Application.Mediatr;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, ICommandAbstraction
{
}

public interface ICommand : IRequest<Result.Result>, ICommandAbstraction
{
}

public interface ICommandAbstraction
{
}