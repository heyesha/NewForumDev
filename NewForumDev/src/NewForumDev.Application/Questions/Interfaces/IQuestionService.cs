using NewForumDev.Contracts.Questions;

namespace NewForumDev.Application.Questions.Interfaces;

public interface IQuestionService
{
    Task<Guid> CreateAsync(CreateQuestionDto questionDto, CancellationToken cancellationToken);
}