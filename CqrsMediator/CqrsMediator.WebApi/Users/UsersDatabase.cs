namespace CqrsMediator.WebApi.Users;

public static class UsersDatabase
{
    public static Dictionary<Guid, string> SeedUsersData() => new()
    {
        [Guid.Empty] = "Empty User",
        [Guid.NewGuid()] = "Random User 1",
        [Guid.NewGuid()] = "Random User 2",
    };
}
