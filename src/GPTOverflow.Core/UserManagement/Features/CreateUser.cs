using CSharpFunctionalExtensions;
using FluentValidation;
using GPTOverflow.Core.CrossCuttingConcerns.Contracts;
using GPTOverflow.Core.CrossCuttingConcerns.Exceptions;
using GPTOverflow.Core.CrossCuttingConcerns.Utils;
using GPTOverflow.Core.UserManagement.Brokers.Persistence;
using GPTOverflow.Core.UserManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace GPTOverflow.Core.UserManagement.Features;

public static class CreateUser
{
    public record Command(string Email) : ICommand<Response>;

    public record Response
        (string Id, string Email, string Username, string Name, bool Suspended, string Status) : BaseDto(Id);

    class CommandValidator : BaseFluentValidator<Command>
    {
        private readonly UserManagementDbContext _context;

        public CommandValidator(UserManagementDbContext context)
        {
            _context = context;
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Invalid email address")
                .MustAsync(EmailMustNotExist).WithMessage("Email already exists");
        }

        private async Task<bool> EmailMustNotExist(string email, CancellationToken cancellationToken)
        {
            var exists =
                await _context.Users.AnyAsync(x => x.EmailAddress == email, cancellationToken: cancellationToken);
            return !exists;
        }
    }

    public class Handler : ICommandHandler<Command, Response>
    {
        private readonly UserManagementDbContext _context;

        public Handler(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            await RuleValidator.ValidateAsync(request, new CommandValidator(_context));

            var memberRole = await _context.Roles.SingleOrDefaultAsync(x => x.Name == UserRole.Member,
                cancellationToken: cancellationToken);
            if (memberRole == null)
            {
                throw new CriticalSystemException("Required member role not found!");
            }

            var user = new ApplicationUser(request.Email)
            {
                RoleId = memberRole.Id
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(new Response(user.Id.ToString(), user.EmailAddress, user.Username, user.Name,
                user.IsSuspended(), user.Status.ToString()));
        }
    }
}