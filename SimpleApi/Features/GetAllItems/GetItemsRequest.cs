namespace SimpleApi.Features.GetAllItems;
using SimpleMediator.Abstractions;

public static class GetItemsRequest
{
    /// <summary>
    /// Represents a request to retrieve all items.
    /// </summary>
    public record Query : IRequest<List<string>>;

    /// <summary>
    /// Handler for processing the <see cref="Query"/> request.
    /// </summary>
    public class Handler : IRequestHandler<Query, List<string>>
    {
        public Task<List<string>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            // Simulate fetching data from a database or other source
            var items = new List<string> { "Item1", "Item2", "Item3" };
            return Task.FromResult(items);
        }
    }
}

