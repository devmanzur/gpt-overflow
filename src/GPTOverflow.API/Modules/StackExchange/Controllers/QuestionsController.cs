using GPTOverflow.API.Modules.CrossCuttingConcerns.Controllers;
using GPTOverflow.API.Modules.CrossCuttingConcerns.Models;
using GPTOverflow.API.Modules.StackExchange.Models;
using GPTOverflow.Core.StackExchange.Features;
using GPTOverflow.Core.StackExchange.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GPTOverflow.API.Modules.StackExchange.Controllers;

public class QuestionsController : ApiController
{
    private readonly IMediator _mediator;

    public QuestionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Envelope<QuestionDto>>> CreateQuestion([FromBody] PostNewQuestionRequest request)
    {
        var result = await _mediator
            .Send(new PostNewQuestion.Command(GetUserId(), request.Title, request.Description, request.Tags));
        return result.IsSuccess ? Ok(Envelope.Ok(result.Value)) : BadRequest(Envelope.Error(result.Error));
    }
}