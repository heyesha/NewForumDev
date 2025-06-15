using Microsoft.EntityFrameworkCore;
using Questions.Application.Abstractions;
using Questions.Contracts.Dtos;
using Questions.Contracts.Responses;
using Questions.Domain;
using Shared.Abstractions;
using Shared.FilesStorage;
using Tags.Contracts;
using Tags.Contracts.Dtos;

namespace Questions.Application.Features.GetQuestionsWithFiltersQuery;

public class GetQuestionsWithFiltersCommandHandler : IQueryHandler<QuestionResponse, GetQuestionsWithFiltersQuery>
{
    private readonly IFilesProvider _filesProvider;
    private readonly ITagsContract _tagsContract;
    private readonly IQuestionsReadDbContext _questionsReadDbContext;
    
    public GetQuestionsWithFiltersCommandHandler(
        IFilesProvider filesProvider,
        ITagsContract tagsContract, 
        IQuestionsReadDbContext questionsReadDbContext)
    {
        _filesProvider = filesProvider;
        _tagsContract = tagsContract;
        _questionsReadDbContext = questionsReadDbContext;
    }
    
    public async Task<QuestionResponse> HandleAsync(
        GetQuestionsWithFiltersQuery query,
        CancellationToken cancellationToken)
    {
        var questions = await _questionsReadDbContext.ReadQuestions
            .Include(q => q.Solution)
            .Skip(query.Dto.Page * query.Dto.PageSize)
            .Take(query.Dto.PageSize)
            .ToListAsync(cancellationToken);
        
        long count = await _questionsReadDbContext.ReadQuestions.LongCountAsync(cancellationToken);
        
        var screenshotIds = questions
            .Where(q => q.ScreenshotId is not null)
            .Select(x => x.ScreenshotId!.Value);
        
        var filesDict = await _filesProvider.GetUrlsByIdsAsync(screenshotIds, cancellationToken);
        
        var questionTags = questions.SelectMany(q => q.Tags);

        var tags = await _tagsContract.GetByIdsAsync(new GetByIdsDto(questionTags.ToArray()), cancellationToken);

        var questionsDto = questions.Select(q => new QuestionDto(
            q.Id,
            q.Title,
            q.Text,
            q.UserId,
            q.ScreenshotId is not null ? filesDict[q.ScreenshotId.Value] : null,
            q.Solution?.Id,
            tags.Select(t => t.Name),
            q.Status.ToRuString()));

        return new QuestionResponse(questionsDto, count);
    }
}