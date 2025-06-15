using Questions.Contracts.Dtos;
using Shared.Abstractions;

namespace Questions.Application.Features.AddAnswerCommand;

public record AddAnswerQuery(Guid QuestionId, AddAnswerDto AddAnswerDto) : ICommand;