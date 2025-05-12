namespace SimpleMediator.Abstractions
{
    /// <summary>
    /// Defines a contract for sending requests and receiving responses asynchronously.
    /// </summary>
    public interface ISender
    {
        /// <summary>
        /// Sends a request asynchronously and returns the corresponding response.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response expected from the request.</typeparam>
        /// <param name="request">The request instance that contains the necessary input data.</param>
        /// <param name="cancellationToken">
        /// A cancellation token that allows the operation to be canceled before completion.
        /// Defaults to <see cref="CancellationToken.None"/>.
        /// </param>
        /// <returns>A <see cref="Task{TResponse}"/> representing the asynchronous operation,
        /// containing the response of type <typeparamref name="TResponse"/>.</returns>
        Task<TResponse> SendAsync<TResponse>(
            IRequest<TResponse> request,
            CancellationToken cancellationToken = default);
    }
}
