using GPTOverflow.Core.Questionnaire.Models.DataTransferObjects;
using MediatR;

namespace GPTOverflow.Core.Questionnaire.Features;

public static class SearchQuestion
{
    public record Query(string Title) : IRequest<QueryResponse>;

    public record QueryResponse
    {
        public List<QuestionDto>? Questions { get; set; }
    };
    
    internal class Handler : IRequestHandler<Query,QueryResponse>
    {
        public Handler()
        {
            
        }
        public Task<QueryResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}