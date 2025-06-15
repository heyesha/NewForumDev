namespace Questions.Contracts.Dtos;

public record UpdateQuestionDto(string Title, string Text, Guid UserId, Guid[] TagIds);