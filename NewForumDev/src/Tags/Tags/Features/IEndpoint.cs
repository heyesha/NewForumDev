using Microsoft.AspNetCore.Routing;

namespace Tags.Features;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}