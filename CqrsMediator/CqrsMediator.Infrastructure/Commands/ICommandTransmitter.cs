namespace CqrsMediator.Infrastructure.Commands;

/// <summary>
/// Represents a command transmitter responsible for transmitting commands to respective receivers.
/// </summary>
public interface ICommandTransmitter
{
    /// <summary>
    /// Asynchronously transmits a command to respective receiver.
    /// </summary>
    /// <typeparam name="TCommand">The type of command to transmit.</typeparam>
    /// <param name="command">The command to be transmitted.</param>
    /// <returns>A value task representing the asynchronous operation.</returns>
    /// /// <exception cref="InvalidOperationException">There is no respective receiver registered.</exception>
    ValueTask TransmitAsync<TCommand>(TCommand command);
}
