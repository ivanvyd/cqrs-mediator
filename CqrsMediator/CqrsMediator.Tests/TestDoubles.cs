using CqrsMediator.Infrastructure.Commands;
using CqrsMediator.Infrastructure.Queries;

namespace CqrsMediator.Tests.TestDoubles
{
    public class TestCommand : ICommand { }

    public class TestQuery : IQuery<TestQueryResult> { }

    public class TestQueryResult
    {
        public string? Data { get; set; }
    }
}
