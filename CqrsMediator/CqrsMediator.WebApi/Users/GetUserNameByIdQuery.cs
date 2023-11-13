using CqrsMediator.Infrastructure.Queries;

namespace CqrsMediator.WebApi.Users;

public sealed class GetUserNameByIdQuery
{
    public Guid UserId { get; set; }

    public sealed class Receiver : IQueryReceiver<GetUserNameByIdQuery, string>
    {
        private readonly Dictionary<Guid, string> _usersNamesByIds;

        public Receiver()
        {
            _usersNamesByIds = UsersDatabase.SeedUsersData();
        }

        public ValueTask<string> ReceiveAsync(GetUserNameByIdQuery query)
        {
            var matchedName = _usersNamesByIds[query.UserId];

            return ValueTask.FromResult(matchedName);
        }
    }
}
