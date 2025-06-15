using Questions.Contracts.Dtos;
using Shared.Abstractions;

namespace Questions.Application.Features.CreateQuestionCommand;

public record CreateQuestionQuery(CreateQuestionDto QuestionDto) : ICommand;