using Microsoft.Extensions.DependencyInjection;
using SimpleMediator.Abstractions;
using System.Reflection;
using System.Linq;

namespace SimpleMediator
{
    /// <summary>
    /// Provides extension methods to register SimpleMediator components in the dependency injection container.
    /// </summary>
    public static class SimpleMediator
    {
        /// <summary>
        /// Registers the SimpleMediator services and request handlers in the DI container.
        /// </summary>
        /// <param name="services">The service collection for dependency injection.</param>
        /// <param name="lifetime">
        /// Specifies how services should be registered: Scoped, Singleton, or Transient.
        /// </param>
        /// <param name="assembly">
        /// The assembly to scan for request handlers. Defaults to the calling assembly if not provided.
        /// </param>
        /// <returns>The modified service collection with SimpleMediator registered.</returns>
        public static IServiceCollection AddSimpleMediator(
            this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Scoped,
            Assembly? assembly = null)
        {
            // Use the calling assembly if none is provided.
            assembly ??= Assembly.GetCallingAssembly();

            // Register the sender service based on the chosen lifetime.
            RegisterService<ISender, Sender>(services, lifetime);

            // Define the generic request handler interface type.
            Type handlerInterfaceType = typeof(IRequestHandler<,>);

            // Find all types that implement IRequestHandler<TRequest, TResponse>.
            var handlerTypes = assembly
                .GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType))
                .SelectMany(type => type.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType)
                    .Select(i => new { Interface = i, Implementation = type }));

            // Register each handler type based on the chosen lifetime.
            foreach (var handlerType in handlerTypes)
            {
                RegisterService(services, handlerType.Interface, handlerType.Implementation, lifetime);
            }

            return services;
        }

        /// <summary>
        /// Registers a service with the specified lifetime in the DI container.
        /// </summary>
        private static void RegisterService<TInterface, TImplementation>(
    IServiceCollection services, ServiceLifetime lifetime)
    where TInterface : class
    where TImplementation : class, TInterface
        {
            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton<TInterface, TImplementation>();
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<TInterface, TImplementation>();
                    break;
                default:
                    services.AddScoped<TInterface, TImplementation>();
                    break;
            }
        }

        /// <summary>
        /// Registers a service with the specified lifetime in the DI container.
        /// </summary>
        private static void RegisterService(IServiceCollection services,
            Type interfaceType, Type implementationType, ServiceLifetime lifetime)
        {
            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(interfaceType, implementationType);
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient(interfaceType, implementationType);
                    break;
                default:
                    services.AddScoped(interfaceType, implementationType);
                    break;
            }
        }
    }
}