using CqrsMediator.Infrastructure.Queries;

namespace CqrsMediator.WebApi.Users;

public sealed class GetUsersQueryReceiver : IQueryReceiver<Dictionary<Guid, string>>
{
    public ValueTask<Dictionary<Guid, string>> ReceiveAsync()
    {
        var usersNamesByIds = UsersDatabase
            .SeedUsersData();

        return ValueTask.FromResult(usersNamesByIds);
    }
}
