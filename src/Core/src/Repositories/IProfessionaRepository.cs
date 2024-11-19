

using VozAmiga.Core.Data.Model;

namespace VozAmiga.Core.Data.Repositories;


public interface IProfissionalRepository : IRepository<Profissional>
{
    /// <summary>
    /// Find a profissional by its email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Profissional?> FindByUserNameAsync(string username, CancellationToken cancellationToken = default);
    public IQueryable<Profissional> FindAsyc(string? queryFor, CancellationToken cancellationToken = default);
    public Task<Profissional?> FindByIdAsyc(Guid id, CancellationToken cancellationToken = default);
    public Task DeleteByIdAsync(Guid profissional, CancellationToken cancellationToken = default);
}
