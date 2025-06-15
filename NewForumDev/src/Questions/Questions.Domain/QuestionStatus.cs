namespace Questions.Domain;

public enum QuestionStatus
{
    /// <summary>
    /// Статут открыт
    /// </summary>
    OPEN,
    
    /// <summary>
    /// Статус решён
    /// </summary>
    RESOLVED,
}

public static class QuestionStatusExtensions
{
    public static string ToRuString(this QuestionStatus status) =>
        status switch
        {
            QuestionStatus.OPEN => "Открыт",
            QuestionStatus.RESOLVED => "Решен",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
}