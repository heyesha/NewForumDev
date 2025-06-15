using Microsoft.AspNetCore.Mvc;
using Questions.Application.Features.AddAnswerCommand;
using Questions.Application.Features.CreateQuestionCommand;
using Questions.Application.Features.GetQuestionsWithFiltersQuery;
using Questions.Contracts.Dtos;
using Questions.Contracts.Responses;
using Questions.Presenters.ResponseExtensions;
using Shared.Abstractions;

namespace Questions.Presenters;

[ApiController]
[Route("[controller]")]
public class QuestionsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromServices] ICommandHandler<Guid, CreateQuestionQuery> commandHandler, 
        [FromBody] CreateQuestionDto request, 
        CancellationToken cancellationToken)
    {
        var command = new CreateQuestionQuery(request);
        
        var result = await commandHandler.HandleAsync(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error.ToResponse());
        }
        
        return Ok(result.Value);
    }

    [HttpPost("{questionId:guid}/answers")]
    public async Task<IActionResult> AddAnswer(
        [FromServices] ICommandHandler<Guid, AddAnswerQuery> commandHandler,
        [FromRoute] Guid questionId,
        [FromBody] AddAnswerDto request,
        CancellationToken cancellationToken)
    {
        var command = new AddAnswerQuery(questionId, request);
        
        var result = await commandHandler.HandleAsync(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error.ToResponse());
        }
        
        return Ok(result.Value);
    }

    public async Task<IActionResult> GetQuestionsWithFilters(
        [FromServices] IQueryHandler<QuestionResponse, GetQuestionsWithFiltersQuery> commandHandler,
        [FromQuery] GetQuestionsDto request,
        CancellationToken cancellationToken)
    {
        var query = new GetQuestionsWithFiltersQuery(request);
        
        var result = await commandHandler.HandleAsync(query, cancellationToken);

        return Ok(result);
    }
}