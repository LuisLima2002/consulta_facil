
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VozAmiga.Api.Data.Database;
using VozAmiga.Api.Utils;
using VozAmiga.Api.Utils.Database;
using VozAmiga.Core.Data.Repositories;

namespace VozAmiga.Api.Infra.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly IDbContext _context;
    public BaseRepository(IDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T model, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await _context.AddAsync(model, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<T> model, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await _context.Set<T>().AddRangeAsync(model, cancellationToken);
    }

    public void Delete(T model, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _context.Remove(model);
    }

    public Task<bool> AnyAsync(Expression<Func<T, bool>> query, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _context.Set<T>().AnyAsync(query, cancellationToken);
    }

    public Task<IEnumerable<T>> FindAsyc(Func<T, bool> query, CancellationToken cancellationToken = default)
    {
        return new Task<IEnumerable<T>>(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            Expression<Func<T, bool>> exp = e => query(e);

            return _context
                .Set<T>()
                .Where(exp)
                .AsNoTrackingWithIdentityResolution();
        });
    }

    public void Update(T model)
    {
        _context.Update(model);
    }

    public async Task<PagedQuery<T>> FindAsync(QueryConfig<T> queryConfig, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        Expression<Func<T, bool>> exp = e => queryConfig.Query(e);

        var queried = _context
                .Set<T>()
                .Where(exp)
                .AsNoTrackingWithIdentityResolution();

        for (int i = 0; i < queryConfig.Includes?.Count; i++)
        {
            queried = queried.Include(queryConfig.Includes[i]);
        }

        var total = await queried.CountAsync(cancellationToken);



        int toSkip = (int)(queryConfig.Page.Size * queryConfig.Page.Number);
        var result = queried
            .Skip(toSkip)
            .Take((int)queryConfig.Page.Size);

        return new PagedQuery<T>(result, total);
    }

    public async Task<PagedQuery<T>> FindAsync(Expression<Func<T, bool>> query, Page page, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var queried = _context
                .Set<T>()
                .Where(query)
                .AsNoTrackingWithIdentityResolution();

        var total = await queried.CountAsync(cancellationToken);

        int toSkip = (int)(page.Size * page.Number);
        var result = queried
            .Skip(toSkip)
            .Take((int)page.Size);

        return new PagedQuery<T>(result, total);
    }
}
