namespace SimpleMediator.Abstractions
{
    /// <summary>
    /// Defines a contract for handling requests asynchronously and returning responses.
    /// Implementations of this interface process a request and return a response of the specified type.
    /// </summary>
    /// <typeparam name="TRequest">The type of request being handled, which must implement <see cref="IRequest{TResponse}"/>.</typeparam>
    /// <typeparam name="TResponse">The type of the response returned after processing the request.</typeparam>
    public interface IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Handles the given request asynchronously and returns the corresponding response.
        /// </summary>
        /// <param name="request">The request instance containing the necessary input data.</param>
        /// <param name="cancellationToken">
        /// An optional cancellation token that allows the operation to be canceled before completion.
        /// Defaults to <see cref="CancellationToken.None"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Task{TResponse}"/> representing the asynchronous operation,
        /// containing the response of type <typeparamref name="TResponse"/>.
        /// </returns>
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}