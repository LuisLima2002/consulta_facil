namespace VozAmiga.Core.Services.Interface.Auth;


public interface IPasswordService
{

    /// <summary>
    /// Handle pwd hashing
    /// </summary>
    /// <param name="password"></param>
    /// <param name="salt"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<string> HandleAsync(string password, out string salt, CancellationToken cancellationToken = default);
    /// <summary>
    ///
    /// </summary>
    /// <param name="provided">
    ///     The password to be validated
    /// </param>
    /// <param name="password">
    ///     Stored password
    /// </param>
    /// <param name="salt">
    /// Random Extra entrophy
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> CompareAsync(string provided, string password, string salt, CancellationToken cancellationToken = default);
}
