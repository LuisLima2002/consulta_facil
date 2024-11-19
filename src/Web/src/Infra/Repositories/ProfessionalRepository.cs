using Microsoft.EntityFrameworkCore;
using VozAmiga.Api.Data.Database;
using VozAmiga.Core.Data.Model;
using VozAmiga.Core.Data.Repositories;

namespace VozAmiga.Api.Infra.Repositories;

public class ProfissionalRepository : BaseRepository<Profissional>, IProfissionalRepository
{
    // private readonly ILogger<ActivityRepository> _logger;

    public ProfissionalRepository(
        // ILogger<ActivityRepository> logger,
        IDbContext context
    ): base(context)
    {
        // _logger = logger;
    }

    ///<inheritdoc/>
    public async Task<Profissional?> FindByUserNameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _context
                        .Set<Profissional>()
                        .Where(x => x.UserName == username)
                        .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task DeleteByIdAsync(Guid profissional, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var a = await _context
            .Set<Profissional>()
            .Where(a => a.Id == profissional)
            .FirstOrDefaultAsync(cancellationToken);

        if (a != null)
            _context.Remove(a);
        else
            throw new Exception("Não há profissional com esse ID");
    }

    public IQueryable<Profissional> FindAsyc(string? queryFor, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _context
            .Set<Profissional>()
            .AsNoTracking()
            .Where(a => a.Name.ToLower().Contains(queryFor != null ? queryFor.ToLower() : ""));
    }

    public Task<Profissional?> FindByIdAsyc(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        // _logger.LogInformation("Querring with {query}", id);
        return _context
            .Set<Profissional>()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
