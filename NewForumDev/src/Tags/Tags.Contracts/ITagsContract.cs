using Tags.Contracts.Dtos;

namespace Tags.Contracts;

public interface ITagsContract
{
    public Task CreateAsync(CreateTagDto request);
    
    Task<IReadOnlyList<TagDto>> GetByIdsAsync(GetByIdsDto dto, CancellationToken cancellationToken);
}