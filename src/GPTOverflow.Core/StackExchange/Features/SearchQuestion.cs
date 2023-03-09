using GPTOverflow.Core.StackExchange.Brokers.Persistence;
using GPTOverflow.Core.StackExchange.Models.Dto;
using MediatR;

namespace GPTOverflow.Core.StackExchange.Features;

public static class SearchQuestion
{
    public record Query(string TitleQuery) : IRequest<QueryResponse>;

    public record QueryResponse
    {
        public List<QuestionDto>? Questions { get; set; }
    };
    
    internal class Handler : IRequestHandler<Query,QueryResponse>
    {
        private readonly StackExchangeDbContext _context;

        public Handler(StackExchangeDbContext context)
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