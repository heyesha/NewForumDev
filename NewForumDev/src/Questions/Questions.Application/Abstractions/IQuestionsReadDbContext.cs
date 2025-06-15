using Questions.Domain;

namespace Questions.Application.Abstractions;

public interface IQuestionsReadDbContext
{
    IQueryable<Question> ReadQuestions { get; }
}