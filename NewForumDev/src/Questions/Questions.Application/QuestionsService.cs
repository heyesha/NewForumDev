using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Questions.Application.Abstractions;
using Questions.Application.Fails;
using Questions.Contracts.Dtos;
using Questions.Domain;
using Shared;
using Shared.Extensions;

namespace Questions.Application;

public class QuestionsService : IQuestionService
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IValidator<CreateQuestionDto> _createQuestionDtoValidator;
    private readonly IValidator<AddAnswerDto> _addAnswerDtoValidator;
    //private readonly ITransactionManager _transactionManager;
    //private readonly IUsersCommunicationService _usersCommunicationService;
    private readonly ILogger<QuestionsService> _logger;

    public QuestionsService(
        IQuestionsRepository questionsRepository, 
        IValidator<CreateQuestionDto> createQuestionDtoValidator,
        IValidator<AddAnswerDto> addAnswerDtoValidator,
        //ITransactionManager transactionManager,
        //IUsersCommunicationService usersCommunicationService, 
        ILogger<QuestionsService> logger)
    {
        _questionsRepository = questionsRepository;
        //_usersCommunicationService = usersCommunicationService;
        //_transactionManager = transactionManager;
        _createQuestionDtoValidator = createQuestionDtoValidator;
        _addAnswerDtoValidator = addAnswerDtoValidator;
        _logger = logger;
    }
    
    public async Task<Result<Guid, Failure>> CreateAsync(CreateQuestionDto questionDto, CancellationToken cancellationToken)
    {
        var validationResult = await _createQuestionDtoValidator.ValidateAsync(questionDto, cancellationToken);
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
            .GetOpenUserQuestionsAsync(questionDto.UserId, cancellationToken);
        if (openUserQuestionsCount > 3)
        {
            return Errors.Questions.TooManyQuestions().ToFailure();
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

    public async Task<Result<Guid, Failure>> AddAnswerAsync(
        Guid questionId, 
        AddAnswerDto addAnswerDto, 
        CancellationToken cancellationToken)
    {
        var validationResult = await _addAnswerDtoValidator.ValidateAsync(addAnswerDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        /*var userRatingResult = await _usersCommunicationService.GetUserRating(addAnswerDto.UserId, cancellationToken);
        if (userRatingResult.IsFailure)
        {
            // здесь может быть логирование
            return userRatingResult.Error;
        }
        
        if (userRatingResult.Value <= 0)
        {
            return Errors.Questions.NotEnoughRating().ToFailure();
        }*/
        
        //var transaction = await _transactionManager.BeginTransactionAsync(cancellationToken);
        
        (_, bool isFailure, Question? question, Failure? error) = await _questionsRepository.GetByIdAsync(questionId, cancellationToken);
        if (isFailure)
        {
            return error;
        }

        var answer = new Answer(Guid.NewGuid(), addAnswerDto.UserId, addAnswerDto.Text, questionId);
        
        question.Answers.Add(answer);
        
        await _questionsRepository.SaveAsync(question, cancellationToken);

        //transaction.Commit();
        
        _logger.LogInformation("Added answer with id: {answerId} to question {questionId}", answer.Id, questionId);

        return answer.Id;
    }
}

public class QuestionCalculator
{
    public UnitResult<Error> Calculate()
    {
        return Error.Failure("", "");
    }
}