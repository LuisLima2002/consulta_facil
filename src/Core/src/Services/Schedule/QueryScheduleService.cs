
// internal imports
using VozAmiga.Core.DTO.ViewModels;
using VozAmiga.Core.DTO.Query;
using VozAmiga.Api.Utils;
using VozAmiga.Core.Data.Model;

namespace VozAmiga.Api.Services.V1;

public interface IQueryScheduleService
{
    public Task<Result<ScheduleQR?>> GetScheduleAsync(string Id, CancellationToken cancellationToken = default);

    public Task<Result<Paginated<ScheduleQR>>> GetScheduleAsync(Query query, CancellationToken cancellationToken = default);
}
