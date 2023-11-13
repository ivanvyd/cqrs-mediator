namespace CqrsMediator.Infrastructure.Queries;

/// <summary>
/// Defines a receiver for processing a query and returning a result.
/// </summary>
/// <typeparam name="TQuery">The type of query to handle.</typeparam>
/// <typeparam name="TQueryResult">The type of result to return.</typeparam>
public interface IQueryReceiver<in TQuery, TQueryResult>
{
    /// <summary>
    /// Asynchronously receives a query and returns a result.
    /// </summary>
    /// <param name="query">The query to be received.</param>
    /// <returns>A value task representing the asynchronous operation with the query result.</returns>
    ValueTask<TQueryResult> ReceiveAsync(TQuery query);
}

/// <summary>
/// Defines a receiver for processing a query with no parameters and returning a result.
/// </summary>
/// <typeparam name="TQueryResult">The type of result to return.</typeparam>
public interface IQueryReceiver<TQueryResult>
{
    /// <summary>
    /// Asynchronously receives a query and returns a result.
    /// </summary>
    /// <returns>A value task representing the asynchronous operation with the query result.</returns>
    ValueTask<TQueryResult> ReceiveAsync();
}