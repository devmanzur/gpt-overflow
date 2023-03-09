using GPTOverflow.Core.CrossCuttingConcerns.Contracts;
using GPTOverflow.Core.CrossCuttingConcerns.Events;
using GPTOverflow.Core.UserManagement.Models;
using MediatR;

namespace GPTOverflow.Core.UserManagement.Events;

public static class UserCreated
{
    public record Event(ApplicationUser User) : IDomainEvent;

    class EventHandler : INotificationHandler<Event>
    {
        private readonly IMediator _mediator;

        public EventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(Event notification, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new NewUserAccountCreated(notification.User.Username), cancellationToken);
        }
    }
}