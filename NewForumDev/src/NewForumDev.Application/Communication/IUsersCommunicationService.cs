using CSharpFunctionalExtensions;
using Shared;

namespace NewForumDev.Application.Communication;

public interface IUsersCommunicationService
{
    Task<Result<long, Failure>> GetUserRating(Guid userId, CancellationToken cancellationToken = default);
}