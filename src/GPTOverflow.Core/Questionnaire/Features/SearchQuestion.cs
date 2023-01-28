using GPTOverflow.Core.Questionnaire.Models;
using GPTOverflow.Core.Questionnaire.Models.DataTransferObjects;
using GPTOverflow.Core.Questionnaire.Persistence;
using MediatR;

namespace GPTOverflow.Core.Questionnaire.Features;

public static class SearchQuestion
{
    public record Query(string TitleQuery) : IRequest<QueryResponse>;

    public record QueryResponse
    {
        public List<QuestionDto>? Questions { get; set; }
    };
    
    internal class Handler : IRequestHandler<Query,QueryResponse>
    {
        private readonly QuestionnaireDbContext _context;

        public Handler(QuestionnaireDbContext context)
        {
            _context = context;
        }
        public async Task<QueryResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            var matchingQuestions = "write raw query here";

            return new QueryResponse();
        }
    }
}