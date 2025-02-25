namespace CloudStore.Domain.Abstractions.Core;

public interface IDataRequest<in TRequest, TResponse>
{
    Task<TResponse> GetAsync(TRequest request, CancellationToken cancellationToken = default);
}