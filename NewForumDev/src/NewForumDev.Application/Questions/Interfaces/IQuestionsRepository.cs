using NewForumDev.Domain.Questions;

namespace NewForumDev.Application.Questions.Interfaces;

public interface IQuestionsRepository
{
    Task<Guid> AddAsync(Question question, CancellationToken cancellationToken);
    
    Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken);
    
    Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken);
    
    Task<Question> GetByIdAsync(Guid questionId, CancellationToken cancellationToken);
    
    Task<int> GetOpenUserQuestionsAsync(Guid userId, CancellationToken cancellationToken);
}