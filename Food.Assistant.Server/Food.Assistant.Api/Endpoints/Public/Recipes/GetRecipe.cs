using Food.Assistant.Api.MinimalApis;
using Microsoft.AspNetCore.Mvc;

namespace Food.Assistant.Api.Endpoints.Public.Recipes;

public class GetRecipe
{
    public record Query(string Id);
    public record Response(string Recipe);
    
    public class Handler : BaseEndpoint<Query, Response> 
    {
        public override string Path => "/recipes/{id}";
        public override HttpMethod HttpMethod => HttpMethod.Get;
        public override async Task<Response> HandleAsync([FromQuery]Query request, HttpContext context, CancellationToken ct)
        {
            return new Response($"{request.Id}");
        }
    }
}