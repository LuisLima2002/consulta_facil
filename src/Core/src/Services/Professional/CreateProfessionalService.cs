using VozAmiga.Core.DTO.Commands;
using VozAmiga.Core.DTO.ViewModels;
using VozAmiga.Api.Utils;

namespace VozAmiga.Core.Services.Interface.Profissionals;


public interface ICreateProfissionalService
{
    /// <summary></summary>
    public Task<Result<string>> HandleAsync(CreateProfissionalCmd cmd, CancellationToken cancellationToken = default);

    public Task<Result> RemoveAsync(string professionalId, CancellationToken cancellationToken = default);

    public Task<Result> UpdateAsync(ProfissionalQR profissionalQR, CancellationToken cancellationToken = default);
    public Task<Result<string>> ResetPasswordAsync(string profissionalId, CancellationToken cancellationToken = default);
    public Task<Result> ChangePasswordAsync(ChangePasswordCmd changePasswordCmd, CancellationToken cancellationToken = default);
}
