using CSharpFunctionalExtensions;
using FluentValidation;
using GPTOverflow.Core.CrossCuttingConcerns.Contracts;
using GPTOverflow.Core.CrossCuttingConcerns.Utils;
using GPTOverflow.Core.StackExchange.Brokers.Persistence;
using GPTOverflow.Core.StackExchange.Models;
using Microsoft.EntityFrameworkCore;

namespace GPTOverflow.Core.StackExchange.Features;

public class PostNewQuestion
{
    public record Command
        (Guid UserId, string Title, string Description, List<string>? Tags) : ICommand<CommandResponse>;

    public record CommandResponse(string Id, string Title, string Description) : BaseDto(Id);

    private class CommandValidator : BaseFluentValidator<Command>
    {
        private readonly StackExchangeDbContext _context;

        public CommandValidator(StackExchangeDbContext context)
        {
            _context = context;
            RuleFor(x => x.UserId).NotNull().NotEmpty().MustAsync(UserExists).WithMessage("Invalid user id");
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(2000).WithMessage("Invalid title");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description must not be empty");
        }

        private async Task<bool> UserExists(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Accounts.AnyAsync(x => x.Id == userId, cancellationToken: cancellationToken);
        }
    }

    public class Handler : ICommandHandler<Command, CommandResponse>
    {
        private readonly StackExchangeDbContext _context;

        public Handler(StackExchangeDbContext context)
        {
            _context = context;
        }

        public async Task<Result<CommandResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            await RuleValidator.ValidateAsync(request, new CommandValidator(_context));

            var question = new Question()
            {
                Title = request.Title,
                Description = request.Description,
                AccountId = request.UserId,
            };

            if (request.Tags != null && request.Tags.Any())
            {
                var tags = await _context
                    .Tags
                    .Where(x => request.Tags.Contains(x.Name))
                    .AsNoTracking()
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken: cancellationToken);

                if (!tags.Any())
                {
                    return Result.Failure<CommandResponse>("Invalid tags");
                }

                question.SetTags(tags);
            }

            _context.Questions.Add(question);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(new CommandResponse(question.Id.ToString(), question.Title, question.Description));
        }
    }
}