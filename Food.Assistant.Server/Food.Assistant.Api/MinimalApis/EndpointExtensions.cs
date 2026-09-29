using System.Reflection;
using Food.Assistant.Api.Endpoints.Public.Recipes;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Food.Assistant.Api.MinimalApis;

public static class EndpointExtensions
{

    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var endpointTypes = assembly.GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false }
                        && typeof(IBaseEndpoint).IsAssignableFrom(t));

        foreach (var endpoint in endpointTypes)
        {
            var descriptor = ServiceDescriptor.Scoped(service: typeof(IBaseEndpoint), implementationType: endpoint);
            services.TryAddEnumerable(descriptor);

            services.AddScoped(endpoint);
        }

        return services;
    }
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        using var scope = builder.ServiceProvider.CreateScope();
        var endpoints = scope.ServiceProvider.GetRequiredService<IEnumerable<IBaseEndpoint>>();

        foreach (var endpoint in endpoints)
        {
            endpoint.Map(builder);
        }
        
        return builder;
    }
    
}