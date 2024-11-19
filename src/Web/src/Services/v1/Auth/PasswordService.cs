
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using VozAmiga.Core.Services.Interface.Auth;

namespace VozAmiga.Api.Services.V1.Auth;


public class PasswordService : IPasswordService
{
    private readonly int _hashMemCost;
    public PasswordService(IConfiguration configuration)
    {
        var size = configuration["Security:HashMem"];
        var scalingFactor = 1;
        if (size != null && size.Length > 2)
        {
            scalingFactor = size[^2..] switch
            {
                "MB" => 1024,
                "GB" => 1024 * 1024,
                _ => 1
            };
            size = size[..^2];
        }
        _hashMemCost = int.TryParse(size, out var memCost) ? memCost : 1024;
        _hashMemCost *= scalingFactor;
    }

    public Task<bool> CompareAsync(string provided, string password, string salt, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(provided == password);
        /* var saltBytes = Encoding.ASCII.GetBytes(salt);
        using var hasher = new Argon2d(Encoding.ASCII.GetBytes(provided));
        hasher.Salt = saltBytes;
        hasher.DegreeOfParallelism = 4;
        hasher.Iterations = 1;
        hasher.MemorySize = _hashMemCost;

        var hashBytes = await hasher.GetBytesAsync(128);
        var hashToCompare = Convert.ToBase64String(hashBytes);

        return hashToCompare == password; */
    }

    public Task<string> HandleAsync(string password, out string salt, CancellationToken cancellationToken)
    {
        salt = "";
        return Task.FromResult(password);
        /* var saltBytes = RandomNumberGenerator.GetBytes(64);
        using var hasher = new Argon2d(Encoding.ASCII.GetBytes(password));
        hasher.Salt = saltBytes;
        hasher.DegreeOfParallelism = 4;
        hasher.Iterations = 1;
        hasher.MemorySize = _hashMemCost;

        salt = Encoding.ASCII.GetString(saltBytes);
        return Task.Run(async () => Encoding.ASCII.GetString(await hasher.GetBytesAsync(128))); */
    }
}
