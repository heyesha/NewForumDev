﻿using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Abstractions;
using Tags.Contracts.Dtos;
using Tags.Database;

namespace Tags.Features;

public sealed class GetByIds
{
    public record GetByIdsQuery(GetByIdsDto dto) : IQuery;
    
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("tags/ids", async (
                GetByIdsDto dto,
                IQueryHandler<IReadOnlyList<TagDto>, GetByIdsQuery> handler,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.HandleAsync(new GetByIdsQuery(dto), cancellationToken);
                
                return Results.Ok(result);
            });
        }
    }

    public sealed class Handler : IQueryHandler<IReadOnlyList<TagDto>, GetByIdsQuery>
    {
        private readonly TagsDbContext _tagsDbContext;

        public Handler(TagsDbContext tagsDbContext)
        {
            _tagsDbContext = tagsDbContext;
        }
        
        public async Task<IReadOnlyList<TagDto>> HandleAsync(GetByIdsQuery query, CancellationToken cancellationToken)
        {
            var tags = await _tagsDbContext.Tags
                .Where(x => query.dto.Ids.Contains(x.Id))
                .ToListAsync(cancellationToken);

            return tags.Select(t => new TagDto(t.Id, t.Name)).ToList();
        }
    }
}