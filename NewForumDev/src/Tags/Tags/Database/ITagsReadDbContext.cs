using Tags.Domain;

namespace Tags.Database;

public interface ITagsReadDbContext
{
    IQueryable<Tag> ReadTags { get; }
}