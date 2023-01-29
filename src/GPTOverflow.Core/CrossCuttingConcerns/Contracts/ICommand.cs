using CSharpFunctionalExtensions;
using MediatR;

namespace GPTOverflow.Core.CrossCuttingConcerns.Contracts;

public interface ICommand<TResponse> : IRequest<Result<TResponse>> where TResponse : BaseDto
{
}

public interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
    where TRequest : ICommand<TResponse> where TResponse : BaseDto
{
    
}