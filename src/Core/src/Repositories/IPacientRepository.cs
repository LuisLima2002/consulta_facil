using VozAmiga.Core.Data.Model;

namespace VozAmiga.Core.Data.Repositories
{
    public interface IPatientRepository : IRepository<Patient>
    {
        // public Task<Patient> DeleteAsync(Patient patient, CancellationToken cancellationToken = default);
        public Task DeleteByIdAsync(Guid patient, CancellationToken cancellationToken = default);
        public Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<Patient?> GetAsync(string queryFor, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Patient>> GetAllAsyc(string queryFor, CancellationToken cancellationToken = default);
        public Task<Patient?> FindByIdAsyc(Guid id, CancellationToken cancellationToken = default);
        public IQueryable<Patient> FindAsyc(string? queryFor, CancellationToken cancellationToken = default);
    }
}
