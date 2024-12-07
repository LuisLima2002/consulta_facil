using VozAmiga.Core.DTO.Commands;
using VozAmiga.Core.DTO.ViewModels;
using VozAmiga.Api.Utils;
using VozAmiga.Core.Data.Model;

namespace VozAmiga.Core.Services.Interface.Profissionals;


public interface ICreateScheduleService
{
    /// <summary></summary>
    public Task<Result<string>> HandleAsync(CreateScheduleCmd cmd, CancellationToken cancellationToken = default);

    public Task<Result> RemoveAsync(string Id, CancellationToken cancellationToken = default);

    public Task<Result> UpdateAsync(ScheduleQR scheduleQR, CancellationToken cancellationToken = default);
}
