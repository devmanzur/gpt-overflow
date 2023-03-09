using FluentValidation;
using GPTOverflow.Core.CrossCuttingConcerns.Contracts;
using GPTOverflow.Core.CrossCuttingConcerns.Utils;
using GPTOverflow.Core.UserManagement.Brokers.Persistence;
using GPTOverflow.Core.UserManagement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GPTOverflow.Core.UserManagement.Features;

public class GetUser
{
    public record Query(string Username) : IRequest<Response>;

    public record Response(string Id, string Email, string Username, string Name, bool Suspended,string Status) : BaseDto(Id);

    class QueryValidator : BaseFluentValidator<Query>
    {
        private readonly UserManagementDbContext _context;

        public QueryValidator(UserManagementDbContext context)
        {
            _context = context;
            RuleFor(x => x.Username).NotNull().NotEmpty().MustAsync(RegisteredUser).WithMessage("Invalid user");
        }

        private Task<bool> RegisteredUser(string username, CancellationToken cancellationToken)
        {
            return _context.Users.AnyAsync(x => x.Username == username, cancellationToken: cancellationToken);
        }
    }

    class Handler : IRequestHandler<Query, Response>
    {
        private readonly UserManagementDbContext _context;

        public Handler(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            await RuleValidator.ValidateAsync(request, new QueryValidator(_context));

            var user = await _context.Users.AsNoTracking()
                .SingleAsync(x => x.Username == request.Username,
                cancellationToken: cancellationToken);
            
            return new Response(user.Id.ToString(), user.EmailAddress, user.Username, user.Name,
                user.IsSuspended(),user.Status.ToString());
        }
    }
}