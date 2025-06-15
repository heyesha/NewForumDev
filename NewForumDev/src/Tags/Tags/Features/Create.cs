using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Tags.Contracts.Dtos;
using Tags.Database;
using Tags.Domain;

namespace Tags.Features;

public sealed class Create
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("tags", HandleAsync);
        }
    }

    public static async Task<IResult> HandleAsync(CreateTagDto request, TagsDbContext tagsDbContext)
    {
        // логика создания тэга и тд
        var tag = new Tag
        {
            Name = request.Name,
        };
        
        await tagsDbContext.AddAsync(tag);

        return Results.Ok(tag.Id);
    }
}