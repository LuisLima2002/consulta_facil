

using VozAmiga.Core.Data.Model;

namespace VozAmiga.Core.Data.Repositories;


public interface IScheduleRepository : IRepository<Schedule>
{
    /// <summary>
    /// Find a profissional by its email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public IQueryable<Schedule> FindAsyc(string? queryFor, CancellationToken cancellationToken = default);
    public Task<Schedule?> FindByIdAsyc(Guid id, CancellationToken cancellationToken = default);
    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
