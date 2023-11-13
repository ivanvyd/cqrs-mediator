using Microsoft.Extensions.DependencyInjection;

namespace CqrsMediator.Infrastructure.Queries;

public sealed class QueryTransmitter : IQueryTransmitter
{
    private readonly IServiceProvider _serviceProvider;

    public QueryTransmitter(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public ValueTask<TQueryResult> TransmitAsync<TQuery, TQueryResult>(TQuery query)
    {
        var handler = _serviceProvider.GetRequiredService<IQueryReceiver<TQuery, TQueryResult>>();

        return handler.ReceiveAsync(query);
    }

    public ValueTask<TQueryResult> TransmitAsync<TQueryResult>()
    {
        var handler = _serviceProvider.GetRequiredService<IQueryReceiver<TQueryResult>>();

        return handler.ReceiveAsync();
    }
}