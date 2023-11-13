using Microsoft.Extensions.DependencyInjection;

namespace CqrsMediator.Infrastructure.Commands;

public sealed class CommandTransmitter : ICommandTransmitter
{
    private readonly IServiceProvider _serviceProvider;

    public CommandTransmitter(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public ValueTask TransmitAsync<TCommand>(TCommand command)
    {
        var handler = _serviceProvider.GetRequiredService<ICommandReceiver<TCommand>>();

        return handler.ReceiveAsync(command);
    }
}