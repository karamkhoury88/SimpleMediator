using SimpleApi.Features.GetAllItems;

namespace SimpleApi.Features;

public static class EndpointExtensions
{
    /// <summary>
    /// Registers all feature-based endpoints in the application.
    /// </summary>
    public static void UseFeatureEndpoints(this IEndpointRouteBuilder endpoints)
    {
        new GetItemsEndpoint().Configure(endpoints);
        // Add other endpoints here as needed
    }
}
