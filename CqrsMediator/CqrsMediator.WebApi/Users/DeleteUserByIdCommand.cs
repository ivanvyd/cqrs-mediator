using CqrsMediator.Infrastructure.Commands;

namespace CqrsMediator.WebApi.Users;

public sealed class DeleteUserByIdCommand
{
    public Guid UserId { get; set; }

    public sealed class Receiver : ICommandReceiver<DeleteUserByIdCommand>
    {
        public ValueTask ReceiveAsync(DeleteUserByIdCommand command)
        {
            return ValueTask.CompletedTask;
        }
    }
}
