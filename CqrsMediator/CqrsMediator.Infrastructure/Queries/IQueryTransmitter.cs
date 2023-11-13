namespace CqrsMediator.Infrastructure.Queries;

/// <summary>
/// Represents a query transmitter responsible for dispatching queries to appropriate receivers.
/// </summary>
public interface IQueryTransmitter
{
    /// <summary>
    /// Asynchronously transmits a query to an appropriate receiver and returns a result.
    /// </summary>
    /// <typeparam name="TQuery">The type of query to transmit.</typeparam>
    /// <typeparam name="TQueryResult">The type of result expected from the query.</typeparam>
    /// <param name="query">The query to be transmitted.</param>
    /// <returns>A value task representing the asynchronous operation.</returns>
    /// /// <exception cref="InvalidOperationException">There is no respective receiver registered.</exception>
    ValueTask<TQueryResult> TransmitAsync<TQuery, TQueryResult>(TQuery query);

    /// <summary>
    /// Asynchronously transmits a query with no parameters to an appropriate receiver and returns a result.
    /// </summary>
    /// <typeparam name="TQueryResult">The type of result expected from the query.</typeparam>
    /// <returns>A value task representing the asynchronous operation.</returns>
    /// /// <exception cref="InvalidOperationException">There is no respective receiver registered.</exception>
    ValueTask<TQueryResult> TransmitAsync<TQueryResult>();
}