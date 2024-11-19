using VozAmiga.Core.DTO.Commands;
using VozAmiga.Api.DTO.ViewModel;
using VozAmiga.Api.Utils;

namespace VozAmiga.Core.Services.Interface.Auth;


public interface IAuthService
{
    /// <summary>
    /// Handle a sign
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Result<ApiCredentials>> HandleAsync(SignInCmd cmd, CancellationToken cancellationToken);
    public string? ReadTokenString(string token);
}
