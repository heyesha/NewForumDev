using Microsoft.AspNetCore.Mvc;
using NewForumDev.Application.Questions;
using NewForumDev.Contracts;
using NewForumDev.Contracts.Questions;

namespace NewForumDev.Presenters.Questions;

[ApiController]
[Route("[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionService _questionService;
    public QuestionsController(IQuestionService questionService)
    {
        _questionService = questionService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateQuestionDto request, CancellationToken cancellationToken)
    {
        var questionId = await _questionService.CreateAsync(request, cancellationToken);
        return Ok(questionId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetQuestionsDto request, CancellationToken cancellationToken)
    {
        return Ok("Questions got");
    }
    
    [HttpGet("{questionId:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid questionId, CancellationToken cancellationToken)
    {
        return Ok("Question got");
    }
    
    [HttpPut("{questionId:guid}")]
    public async Task<IActionResult> UpdateAsync(
    [FromRoute] Guid questionId, 
    [FromBody] UpdateQuestionDto request, 
    CancellationToken cancellationToken)
    {
        return Ok("Question updated");
    }
    
    [HttpDelete("{questionId:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid questionId, CancellationToken cancellationToken)
    {
        return Ok("Question deleted");
    }

    [HttpPut("{questionId:guid}/solution")]
    public async Task<IActionResult> SelectSolutionAsync(
    [FromRoute] Guid questionId, 
    [FromQuery] Guid answerId, 
    CancellationToken cancellationToken)
    {
        return Ok("Solution selected");
    }
    
    [HttpPost("{questionId:guid}/answers")]
    public async Task<IActionResult> AddAnswerAsync(
    [FromRoute] Guid questionId, 
    [FromBody] AddAnswerDto request, 
    CancellationToken cancellationToken)
    {
        return Ok("Answer added");
    }
}