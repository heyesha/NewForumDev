namespace NewForumDev.Contracts;

public record UpdateQuestionDto(string Title, string Text, Guid UserId, Guid[] TagIds);