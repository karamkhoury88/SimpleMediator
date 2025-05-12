using SimpleMediator.Abstractions;

namespace SimpleApi.Features.GetAllItems;

/// <summary>
/// Defines the endpoint for fetching items.
/// </summary>
public class GetItemsEndpoint
{
    /// <summary>
    /// Configures the API route for fetching items.
    /// </summary>
    public void Configure(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("api/items", HandleAsync);
    }

    /// <summary>
    /// Handles the request to retrieve all items.
    /// </summary>
    private static async Task<IResult> HandleAsync(ISender sender)
    {
        var items = await sender.SendAsync(new GetItemsRequest.Query());
        return Results.Ok(items);
    }
}