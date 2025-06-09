using NewForumDev.Contracts.Questions;

namespace NewForumDev.Application.Questions;

public interface IQuestionService
{
    Task<Guid> CreateAsync(CreateQuestionDto questionDto, CancellationToken cancellationToken);
}