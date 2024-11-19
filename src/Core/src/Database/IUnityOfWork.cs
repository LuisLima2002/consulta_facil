

namespace VozAmiga.Api.Data.Database;
public interface IUnityOfWork: IDisposable, IAsyncDisposable
{
    public Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    public Task<int> CommitAsync(CancellationToken cancellationToken = default);
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public Task RollbackAsync(CancellationToken cancellationToken = default);
}
