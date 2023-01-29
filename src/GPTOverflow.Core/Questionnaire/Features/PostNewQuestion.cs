using CSharpFunctionalExtensions;
using FluentValidation;
using GPTOverflow.Core.CrossCuttingConcerns.Contracts;
using GPTOverflow.Core.CrossCuttingConcerns.Utils;
using GPTOverflow.Core.Questionnaire.Models;
using GPTOverflow.Core.Questionnaire.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GPTOverflow.Core.Questionnaire.Features;

public class PostNewQuestion
{
    public record Command
        (Guid UserId, string Title, string Description, List<string>? Tags) : ICommand<CommandResponse>;

    public record CommandResponse(string Id, string Title, string Description) : BaseDto(Id);

    private class CommandValidator : BaseFluentValidator<Command>
    {
        private readonly QuestionnaireDbContext _context;

        public CommandValidator(QuestionnaireDbContext context)
        {
            _context = context;
            RuleFor(x => x.UserId).NotNull().NotEmpty().MustAsync(VerifyUserExistsAsync).WithMessage("Invalid user id");
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(2000).WithMessage("Invalid title");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description must not be empty");
        }

        private async Task<bool> VerifyUserExistsAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Accounts.AnyAsync(x => x.Id == userId, cancellationToken: cancellationToken);
        }
    }

    public class Handler : ICommandHandler<Command, CommandResponse>
    {
        private readonly QuestionnaireDbContext _context;

        public Handler(QuestionnaireDbContext context)
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