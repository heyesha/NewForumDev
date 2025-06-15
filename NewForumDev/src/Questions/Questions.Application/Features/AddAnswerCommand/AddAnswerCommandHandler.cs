using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Questions.Application.Abstractions;
using Questions.Contracts.Dtos;
using Questions.Domain;
using Shared;
using Shared.Abstractions;
using Shared.Extensions;

namespace Questions.Application.Features.AddAnswerCommand;

public class AddAnswerCommandHandler : ICommandHandler<Guid, AddAnswerQuery>
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IValidator<AddAnswerDto> _validator;
    //private readonly ITransactionManager _transactionManager;
    //private readonly IUsersCommunicationService _usersCommunicationService;
    private readonly ILogger<QuestionsService> _logger;
    
    public AddAnswerCommandHandler(
        IQuestionsRepository questionsRepository, 
        IValidator<AddAnswerDto> validator, 
        //ITransactionManager transactionManager, 
        //IUsersCommunicationService usersCommunicationService,
        ILogger<QuestionsService> logger)
    {
        _questionsRepository = questionsRepository;
        _validator = validator;
        _logger = logger;
        //_transactionManager = transactionManager;
        //_usersCommunicationService = usersCommunicationService;
    }
    
    public async Task<Result<Guid, Failure>> HandleAsync(
        AddAnswerQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query.AddAnswerDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        /*var userRatingResult = await _usersCommunicationService
            .GetUserRating(command.AddAnswerDto.UserId, cancellationToken);
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
        
        (_, bool isFailure, Question? question, Failure? error) = await _questionsRepository
            .GetByIdAsync(query.QuestionId, cancellationToken);
        if (isFailure)
        {
            return error;
        }

        var answer = new Answer(Guid.NewGuid(), query.AddAnswerDto.UserId, query.AddAnswerDto.Text, query.QuestionId);
        
        question.Answers.Add(answer);
        
        await _questionsRepository.SaveAsync(question, cancellationToken);

        //transaction.Commit();
        
        _logger.LogInformation("Added answer with id: {answerId} to question {questionId}", answer.Id, query.QuestionId);

        return answer.Id;
    }
}