using MediatR;

namespace GPTOverflow.Core.CrossCuttingConcerns.Persistence;

public class CrossCuttingDomainEventDispatcher : BaseDomainEventDispatcher
{
    public CrossCuttingDomainEventDispatcher(IMediator mediator) : base(mediator)
    {
    }
}