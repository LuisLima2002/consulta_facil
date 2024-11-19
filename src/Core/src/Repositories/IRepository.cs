
using System.Linq.Expressions;
using VozAmiga.Api.Utils;
using VozAmiga.Api.Utils.Database;

namespace VozAmiga.Core.Data.Repositories;


public interface IRepository<T> where T : class
{
    public Task AddAsync(T model, CancellationToken cancellationToken = default);
    public Task AddRangeAsync(IEnumerable<T> model, CancellationToken cancellationToken = default);
    public void Delete(T model, CancellationToken cancellationToken = default);

    public Task<bool> AnyAsync(Expression<Func<T, bool>> query, CancellationToken cancellationToken = default);

    public Task<IEnumerable<T>> FindAsyc(Func<T, bool> query, CancellationToken cancellationToken = default);

    public Task<PagedQuery<T>> FindAsync(QueryConfig<T> config,  CancellationToken cancellationToken = default);

    public void Update(T model);
}
