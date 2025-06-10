using NewForumDev.Application.Questions;
using NewForumDev.Application.Questions.Interfaces;
using NewForumDev.Domain.Questions;

namespace NewForumDev.Infrastructure.PostgreSQL.Repositories;

public class QuestionsSqlRepository : IQuestionsRepository
{
    public async Task<Guid> AddAsync(Question question, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

    }

    public async Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

    }

    public async Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

    }

    public async Task<Question> GetByIdAsync(Guid questionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

    }

    public async Task<int> GetOpenUserQuestionsAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

    }
}