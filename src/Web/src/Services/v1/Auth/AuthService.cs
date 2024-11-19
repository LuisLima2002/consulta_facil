using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VozAmiga.Core.Data.Model;
using VozAmiga.Core.Data.Repositories;
using VozAmiga.Core.DTO.Commands;
using VozAmiga.Api.DTO.ViewModel;
using VozAmiga.Core.Services.Interface.Auth;
using VozAmiga.Api.Utils;
using VozAmiga.Api.Infra.Repositories;

namespace VozAmiga.Api.Services.V1.Auth;


public class AuthService : IAuthService
{
    private readonly IProfissionalRepository _profissionalRepository;
    private readonly IPasswordService _passwordService;
    private readonly byte[] _privateKey;

    public AuthService(
        IPasswordService passwordService,
        IProfissionalRepository profissionalRepository,
        IConfiguration configuration
    )
    {
        _passwordService = passwordService;
        _profissionalRepository = profissionalRepository;
        var tmp = configuration.GetValue<string>("Keys:PrivateRSA") ?? "sndfgsdfjkgskdfgjksdfgsdkjfgkjsdjkfgjdfgjk";
        _privateKey = Encoding.ASCII.GetBytes(tmp);
    }

    public async Task<Result<ApiCredentials>> HandleAsync(SignInCmd cmd, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(cmd.UserName))
            return new Error("Precisa informar o username!");

        var res = await _HandleSignIn(cmd, cancellationToken);

        if (!res.IsSuccess) //
            return res.IsException? res.Exception! : res.Error!;

        return _GenerateTokenString(res.Value!);
    }

    public string? ReadTokenString(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var id = jwtToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        return id;
    }

    private ApiCredentials _GenerateTokenString(Profissional user)
    {
        var signCreatendial = new SigningCredentials(
            new RsaSecurityKey(new RSACryptoServiceProvider(2048)),
            SecurityAlgorithms.RsaSha256Signature
        );
        var isPatient = user is Patient;
        var tokenConfig = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim("id", user.Id.ToString()),
                new Claim("user", user.Name),
                new Claim("patient", $"{isPatient}"),
            ]),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = signCreatendial
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenConfig);
        var tokenString = handler.WriteToken(token);

        return new ApiCredentials(tokenString, isPatient, Name: user.Name);
    }

    private async Task<Result<Profissional>> _HandleSignIn(SignInCmd cmd, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(cmd.Password))
            return new Error("Precisa informar a senha!");

        try
        {
            var person = await _profissionalRepository.FindByUserNameAsync(cmd.UserName, cancellationToken);
            if (person == null || !await _passwordService.CompareAsync(cmd.Password, person.Password!, person.Salt!, cancellationToken))
                return new Error("E-mail ou senha inválidos");

            return person;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
