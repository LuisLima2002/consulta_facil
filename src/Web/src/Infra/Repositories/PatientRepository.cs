
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VozAmiga.Api.Data.Database;
using VozAmiga.Core.Data.Model;
using VozAmiga.Core.Data.Repositories;
using VozAmiga.Core.DTO.ViewModels;
using VozAmiga.Api.Utils;

namespace VozAmiga.Api.Infra.Repositories;

public class PatientRepository : BaseRepository<Patient>, IPatientRepository
{
    private readonly ILogger<PatientRepository> _logger;

    public PatientRepository(
        ILogger<PatientRepository> logger,
        IDbContext context
    ) : base(context)
    {
        _logger = logger;
    }

    public async Task DeleteByIdAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var a = await _context
            .Set<Patient>()
            .Where(a => a.Id == patientId)
            .FirstOrDefaultAsync(cancellationToken);

        if (a != null)
             _context.Remove(a);
        else
            throw new Exception("Não há paciente com esse ID");
    }

    public async Task Update(Patient patient, CancellationToken cancellationToken = default)
    {
       await Task.Run(() => _context.Update(patient));
    }

    public IQueryable<Patient> FindAsyc(string? queryFor, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _context
            .Set<Patient>()
            .AsNoTracking()
            .Where(a => a.Name.ToLower().Contains(queryFor!=null ? queryFor.ToLower() : ""));
    }

    public Task<Patient?> FindOneAsyc(string queryFor, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _context
            .Set<Patient>()
            .AsNoTracking()
            // .Where(a => a.QueryMe.Contains(queryFor))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<Patient?> FindByIdAsyc(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        // _logger.LogInformation("Querring with {query}", id);
        return _context
            .Set<Patient>()
            .AsNoTracking()
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Patient?> GetAsync(string queryFor, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Patient>> GetAllAsyc(string queryFor, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

}
