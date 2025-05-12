using SimpleMediator.Abstractions;
using Microsoft.Extensions.DependencyInjection;


namespace SimpleMediator
{
    /// <summary>
    /// Sender class responsible for dispatching requests to the appropriate request handler.
    /// It uses dependency injection to resolve handlers dynamically.
    /// </summary>
    internal class Sender(IServiceProvider serviceProvider) : ISender
    {
        /// <summary>
        /// Sends a request and retrieves a response from the corresponding IRequestHandler.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response expected.</typeparam>
        /// <param name="request">The request instance containing the necessary data.</param>
        /// <param name="cancellationToken">Optional cancellation token to handle request cancellation.</param>
        /// <returns>A Task representing the asynchronous operation with the expected response type.</returns>
        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            // Determine the corresponding handler type based on request type and expected response type.
            Type handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));

            // Resolve the handler instance from the service provider.
            dynamic handler = serviceProvider.GetRequiredService(handlerType);

            // Invoke the handler's HandleAsync method and pass the request along with the cancellation token.
            return handler.HandleAsync((dynamic)request, cancellationToken);
        }
    }
}
