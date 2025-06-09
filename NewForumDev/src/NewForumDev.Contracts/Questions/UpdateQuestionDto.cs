namespace NewForumDev.Contracts.Questions;

public record UpdateQuestionDto(string Title, string Text, Guid UserId, Guid[] TagIds);