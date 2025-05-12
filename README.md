# SimpleMediator Documentation

SimpleMediator is a lightweight and easy-to-use mediator library designed for .NET applications. It simplifies message-based communication between different parts of your application without direct dependencies.

## Features
- **Simple and lightweight** – Minimal performance overhead.
- **Supports multiple handlers** – Allows multiple consumers for the same message.
- **Asynchronous processing** – Built-in support for async operations.
- **Decoupled architecture** – Reduces tight coupling in your codebase.

## Installation
You can install SimpleMediator via NuGet:

```shell
dotnet add package SimpleMediator
```

## Usage
### 1. Define a request
Create a request that will be handled.

```csharp
public record Query : IRequest<List<string>>;
```

### 2. Implement a handler
Define a handler that will process the request.

```csharp
 public class Handler : IRequestHandler<Query, List<string>>
 {
     public Task<List<string>> HandleAsync(Query request, CancellationToken cancellationToken)
     {
         // Simulate fetching data from a database or other source
         var items = new List<string> { "Item1", "Item2", "Item3" };
         return Task.FromResult(items);
     }
 }
```

### 3. Register the mediator
Set up the mediator in your application.

```csharp
builder.Services.AddSimpleMediator(ServiceLifetime.Scoped);
```

### 4. Send a message
Use the mediator to send messages.

```csharp
private static async Task<IResult> HandleAsync(ISender sender)
{
    var items = await sender.SendAsync(new GetItemsRequest.Query());
    return Results.Ok(items);
}
```

## License
SimpleMediator is licensed under the MIT License. See [LICENSE](LICENSE) for details.

## Contributing
Contributions are welcome! Feel free to submit issues or pull requests to improve SimpleMediator.