namespace SimpleMediator.Abstractions
{
    /// <summary>
    /// Represents a request that will be handled asynchronously and produce a response of type <typeparamref name="TResponse"/>.
    /// Implementations of this interface define the data and behavior needed for processing within a mediator framework.
    /// </summary>
    /// <typeparam name="TResponse">The type of the expected response.</typeparam>
    public interface IRequest<TResponse>
    {
        // Marker interface for request types; does not contain any members.
        // Implementing classes should define request-specific properties and behaviors.
    }
}