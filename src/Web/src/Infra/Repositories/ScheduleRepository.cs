using Microsoft.EntityFrameworkCore;
using VozAmiga.Api.Data.Database;
using VozAmiga.Core.Data.Model;
using VozAmiga.Core.Data.Repositories;

namespace VozAmiga.Api.Infra.Repositories;

public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
{
    // private readonly ILogger<ActivityRepository> _logger;

    public ScheduleRepository(
        // ILogger<ActivityRepository> logger,
        IDbContext context
    ): base(context)
    {
        // _logger = logger;
    }

    public async Task DeleteByIdAsync(Guid schedule, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var a = await _context
            .Set<Schedule>()
            .Where(a => a.Id == schedule)
            .FirstOrDefaultAsync(cancellationToken);

        if (a != null)
            _context.Remove(a);
        else
            throw new Exception("Não há profissional com esse ID");
    }

    public IQueryable<Schedule> FindAsyc(string? queryFor, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _context
            .Set<Schedule>()
            .AsNoTracking()
            .Where(a => (a.PatientName + a.PatientId + a.Date.ToString()+a.Reason+a.ScheduleType).ToLower().Contains(queryFor != null ? queryFor.ToLower() : ""));
    }

    public Task<Schedule?> FindByIdAsyc(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        // _logger.LogInformation("Querring with {query}", id);
        return _context
            .Set<Schedule>()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
