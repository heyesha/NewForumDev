using FluentValidation;
using Microsoft.Extensions.Logging;
using NewForumDev.Contracts.Questions;
using NewForumDev.Domain.Questions;

namespace NewForumDev.Application.Questions;

public class QuestionsService : IQuestionService
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly ILogger<QuestionsService> _logger;
    private readonly IValidator<CreateQuestionDto> _validator;

    public QuestionsService(
        IQuestionsRepository questionsRepository, 
        IValidator<CreateQuestionDto> validator,
        ILogger<QuestionsService> logger)
    {
        _questionsRepository = questionsRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Guid> CreateAsync(CreateQuestionDto questionDto, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(questionDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        int openUserQuestionsCount = await _questionsRepository
            .GetOpenUserQuestionsAsync(questionDto.UserId, cancellationToken);
        if (openUserQuestionsCount > 3)
        {
            throw new Exception("Пользователь не может открыть больше трёх вопросов.");
        }
        
        var questionId = Guid.NewGuid();

        var question = new Question(
            questionId, 
            questionDto.Title, 
            questionDto.Text, 
            questionDto.UserId, 
            null,
            questionDto.TagIds);
        
        await _questionsRepository.AddAsync(question, cancellationToken);
        
        _logger.LogInformation("Created question with id: {questionId}", questionId);

        return questionId;
    }
}