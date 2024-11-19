
// internal imports
using VozAmiga.Core.DTO.ViewModels;
using VozAmiga.Core.DTO.Query;
using VozAmiga.Api.Utils;

namespace VozAmiga.Api.Services.V1;

public interface IQueryPatientService
{
    public Task<Result<PatientQR?>> GetPatientAsync(string patientId, CancellationToken cancellationToken = default);

    public Task<Result<Paginated<PatientQR>>> GetPatientsAsync(PersonQuery query, CancellationToken cancellationToken = default);
}
