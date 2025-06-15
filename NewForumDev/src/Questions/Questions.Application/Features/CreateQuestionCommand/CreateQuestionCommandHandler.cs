using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Questions.Application.Abstractions;
using Questions.Application.Fails;
using Questions.Contracts.Dtos;
using Questions.Domain;
using Shared;
using Shared.Abstractions;
using Shared.Extensions;

namespace Questions.Application.Features.CreateQuestionCommand;

public class CreateQuestionCommandHandler : ICommandHandler<Guid, CreateQuestionQuery>
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IValidator<CreateQuestionDto> _validator;
    private readonly ILogger<QuestionsService> _logger;
    
    public CreateQuestionCommandHandler(
        IQuestionsRepository questionsRepository, 
        IValidator<CreateQuestionDto> validator,
        ILogger<QuestionsService> logger) 
    {
        _logger = logger;
        _questionsRepository = questionsRepository;
        _validator = validator;
    }
    
    public async Task<Result<Guid, Failure>> HandleAsync(CreateQuestionQuery query, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query.QuestionDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }
        
        var calculator = new QuestionCalculator();

        var calculateResult = calculator.Calculate();
        if (calculateResult.IsFailure)
        {
            return calculateResult.Error.ToFailure();
        }
        
        int openUserQuestionsCount = await _questionsRepository
            .GetOpenUserQuestionsAsync(query.QuestionDto.UserId, cancellationToken);
        if (openUserQuestionsCount > 3)
        {
            return Errors.Questions.TooManyQuestions().ToFailure();
        }
        
        var questionId = Guid.NewGuid();

        var question = new Question(
            questionId, 
            query.QuestionDto.Title, 
            query.QuestionDto.Text, 
            query.QuestionDto.UserId, 
            null,
            query.QuestionDto.TagIds);
        
        await _questionsRepository.AddAsync(question, cancellationToken);
        
        _logger.LogInformation("Created question with id: {questionId}", questionId);

        return questionId;
    }
}