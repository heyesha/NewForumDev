using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace Questions.Application.Features.SelectSolutionCommand;

public record SelectSolutionQuery() : ICommand;

public class SelectSolutionValidator : ICommandHandler<Guid, SelectSolutionQuery>
{
    public async Task<Result<Guid, Failure>> HandleAsync(SelectSolutionQuery query, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        
        return id;
    }
}