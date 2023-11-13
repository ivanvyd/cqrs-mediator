namespace CqrsMediator.Infrastructure.Commands;

/// <summary>
/// Represents a receiver for a specific type of command.
/// </summary>
/// <typeparam name="TCommand">The type of command to receive.</typeparam>
public interface ICommandReceiver<in TCommand>
{
    /// <summary>
    /// Asynchronously receives a command.
    /// </summary>
    /// <param name="command">The command to be received.</param>
    /// <returns>A value task representing the asynchronous operation.</returns>
    ValueTask ReceiveAsync(TCommand command);
}