using CSharpFunctionalExtensions;
using Questions.Contracts.Dtos;
using Shared;

namespace Questions.Application.Abstractions;

public interface IQuestionService
{
    /// <summary>
    /// Создание вопроса
    /// </summary>
    /// <param name="questionDto">DTO для создания вопроса.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат работы метода, либо ID созданного вопроса, либо список ошибок</returns>
    Task<Result<Guid, Failure>> CreateAsync(CreateQuestionDto questionDto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавление ответа на вопрос
    /// </summary>
    /// <param name="questionId">ID вопроса</param>
    /// <param name="answerDto">DTO для добавления ответа на вопрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат работы метода - либо ID созданного ответа, либо список ошибок</returns>
    Task<Result<Guid, Failure>> AddAnswerAsync(Guid questionId, AddAnswerDto answerDto, CancellationToken cancellationToken);
}