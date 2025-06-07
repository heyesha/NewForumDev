using Microsoft.AspNetCore.Mvc;
using NewForumDev.Contracts;

namespace NewForumDev.Presenters.Controllers;

[ApiController]
[Route("[controller]")]
public class QuestionsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateQuestionDto request, CancellationToken cancellationToken)
    {
        return Ok("question created");
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