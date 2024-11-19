
// internal imports
using VozAmiga.Core.DTO.ViewModels;
using VozAmiga.Core.DTO.Query;
using VozAmiga.Api.Utils;

namespace VozAmiga.Api.Services.V1;

public interface IQueryProfissionalService
{
    public Task<Result<ProfissionalQR?>> GetProfissionalAsync(string professionalId, CancellationToken cancellationToken = default);

    public Task<Result<Paginated<ProfissionalQR>>> GetProfissionalsAsync(PersonQuery query, CancellationToken cancellationToken = default);
}
