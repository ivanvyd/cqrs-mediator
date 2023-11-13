using CqrsMediator.Infrastructure.Commands;
using CqrsMediator.Infrastructure.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace CqrsMediator.Infrastructure;

public static class CqrsAndMediatorStartupExtensions
{
    /// <summary>
    /// Adds custom CQRS and Mediator services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="scanAssembly">The assembly to scan for command and query handlers.</param>
    public static void AddCustomCqrsAndMediator(this IServiceCollection services, Assembly scanAssembly)
    {
        // Register Command related services
        AddCommands(services, scanAssembly);

        // Register Query related services
        AddQueries(services, scanAssembly);
    }

    private static void AddCommands(IServiceCollection services, Assembly scanAssembly)
    {
        services.TryAddScoped<ICommandTransmitter, CommandTransmitter>();

        // Registering all command receivers found in the provided assembly
        RegisterReceivers(services, scanAssembly, typeof(ICommandReceiver<>));
    }

    private static void AddQueries(IServiceCollection services, Assembly scanAssembly)
    {
        services.TryAddScoped<IQueryTransmitter, QueryTransmitter>();

        // Registering all query receivers found in the provided assembly
        RegisterReceivers(services, scanAssembly, typeof(IQueryReceiver<,>));
        RegisterReceivers(services, scanAssembly, typeof(IQueryReceiver<>));
    }

    // Method to register all receivers of a specific type found in the provided assembly
    private static void RegisterReceivers(IServiceCollection services, Assembly scanAssembly, Type receiverType)
    {
        var receiversInterfaceAndImplmentationTypes = scanAssembly
            .GetTypes()
            .Where(type => !type.IsAbstract
                && !type.IsInterface
                && Array.Exists(type.GetInterfaces(), interfaceType => IsGenericTypeAsReceiver(interfaceType, receiverType)))
            .Select(receiverImplementationType => new
            {
                ReceiverInterfaceType = receiverImplementationType
                    .GetInterfaces()
                    .Single(interfaceType => IsGenericTypeAsReceiver(interfaceType, receiverType)),
                ReceiverImplementationType = receiverImplementationType,
            });

        foreach (var item in receiversInterfaceAndImplmentationTypes)
        {
            services.AddScoped(item.ReceiverInterfaceType, item.ReceiverImplementationType);
        }
    }

    // Utility method to check if a type is a generic type as specified
    private static bool IsGenericTypeAsReceiver(Type interfaceType, Type receiverType)
        => interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == receiverType;
}
