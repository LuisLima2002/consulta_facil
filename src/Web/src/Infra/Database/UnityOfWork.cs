
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using VozAmiga.Api.Data.Database;

namespace VozAmiga.Api.Infra.Database;

public class UnityOfWork : IUnityOfWork
{
    private readonly IDbContext _context;
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    public UnityOfWork(IDbContext context)
    {
        _context = context;
        _transaction = null;
        _disposed = false;
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _transaction = await _context.BeginTransactionAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (_transaction != null)
        {
            await _transaction.CommitAsync(cancellationToken);
            lock(_transaction)
            {
                _transaction = null;
            }
            return 0;
        }
        else
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _transaction?.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (!_disposed)
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
            _disposed = true;
        }
    }

}
