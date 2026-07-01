namespace Floristo.SharedAbstractions.Queries;

public interface IQueryDispatcher
{
    Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query);
}