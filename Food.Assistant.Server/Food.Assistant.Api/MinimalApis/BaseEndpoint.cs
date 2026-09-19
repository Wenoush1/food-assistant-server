using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Food.Assistant.Api.MinimalApis;

public abstract class BaseEndpoint<TRequest, TResponse> : IBaseEndpoint where TRequest : notnull
{
    public abstract HttpMethod HttpMethod { get; }
    public abstract string Path { get; }
    public abstract Task<TResponse> HandleAsync(TRequest request, HttpContext context, CancellationToken ct);

    public void Map(IEndpointRouteBuilder builder)
    {
        if (HttpMethod == HttpMethod.Get || HttpMethod == HttpMethod.Delete)
        {
            builder.MapMethods(Path, [HttpMethod.Method],
                ([AsParameters] TRequest request, HttpContext context, CancellationToken ct)
                    => HandleAsync(request, context, ct));
        }
        else
        {
            builder.MapMethods(Path, [HttpMethod.Method],
                ([FromBody] TRequest request, HttpContext context, CancellationToken ct)
                    => HandleAsync(request, context, ct));
        }
    }
}