using GPTOverflow.Core.CrossCuttingConcerns.Events;
using GPTOverflow.Core.StackExchange.Brokers.Persistence;
using GPTOverflow.Core.StackExchange.Models;
using MediatR;

namespace GPTOverflow.Core.StackExchange.Events;

public static class AccountCreated
{
    class EventHandler : INotificationHandler<NewUserAccountCreated>
    {
        private readonly StackExchangeDbContext _context;

        public EventHandler(StackExchangeDbContext context)
        {
            _context = context;
        }
        public async Task Handle(NewUserAccountCreated notification, CancellationToken cancellationToken)
        {
            var account = new Account(notification.Username);
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}