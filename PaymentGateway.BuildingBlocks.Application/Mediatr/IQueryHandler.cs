using MediatR;
using PaymentGateway.BuildingBlocks.Application.Result;

namespace PaymentGateway.BuildingBlocks.Application.Mediatr;

public interface IQueryHandler<in TQuery,TResponse> : IRequestHandler<TQuery, Result<TResponse>>
where TQuery : IQuery<TResponse>
{
    
}