

using System;
using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using VozAmiga.Api.Data.Database;
using VozAmiga.Core.Data.Model;
using VozAmiga.Core.Data.Repositories;
using VozAmiga.Core.DTO.Commands;
using VozAmiga.Core.DTO.ViewModels;
using VozAmiga.Api.Infra.Database;
using VozAmiga.Api.Infra.Repositories;
using VozAmiga.Core.Services.Interface.Auth;
using VozAmiga.Core.Services.Interface.Patient;
using VozAmiga.Core.Services.Interface.Profissionals;
using VozAmiga.Api.Utils;

namespace VozAmiga.Api.Services.V1;


public class CreateProfissionalService : ICreateProfissionalService
{
    private readonly IProfissionalRepository _repository;
    private readonly IDbContext _context;
    private readonly IPasswordService _passwordService;
    private readonly IAuthService _authService;
    private readonly IUnityOfWork _unityOfWork;

    public CreateProfissionalService(
        IProfissionalRepository repository,
        IDbContext context,
        IPasswordService passwordService,
        IAuthService authService,
        IUnityOfWork unityOfWork
    )
    {
        _repository = repository;
        _context = context;
        _passwordService = passwordService;
        _authService = authService;
        _unityOfWork = unityOfWork;
    }

    private static Random random = new Random();

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    /// <inheritdoc />
    public async Task<Result<string>> HandleAsync(CreateProfissionalCmd cmd, CancellationToken cancellationToken = default)
    {
        var duplicatedEmail = await _repository.FindByUserNameAsync(cmd.UserName, cancellationToken);
        if (duplicatedEmail != null)
            return new Error("Este username já possui um cadastro na base");

        var firstPassword = RandomString(25);
        var pwdHash = await _passwordService.HandleAsync(firstPassword, out string salt, cancellationToken);

        var newProf = new Profissional
        {
            Name = cmd.Name,
            UserName = cmd.UserName,
            JobPosition = cmd.JobPosition,
            Permission = cmd.Permission,
            Password = pwdHash,
            Salt = salt,
        };

        await _repository.AddAsync(newProf, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(firstPassword);
    }

    public async Task<Result> UpdateAsync(ProfissionalQR profissionalQR, CancellationToken cancellationToken)
    {
        try
        {
            if (Guid.TryParse(profissionalQR.Id, out var id))
            {
                var profissional = await _repository.FindByIdAsyc(id);
                profissional!.Name = profissionalQR.Name;
                profissional.UserName = profissionalQR.UserName;
                _repository.Update(profissional);
                await _unityOfWork.SaveChangesAsync(cancellationToken);
                return Result.Success;
            }
            return new Error("Invalid Id");
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

    public async Task<Result> ChangePasswordAsync(ChangePasswordCmd changePasswordCmd, CancellationToken cancellationToken)
    {
        try
        {
            var idString  = _authService.ReadTokenString(changePasswordCmd.token);

            if (Guid.TryParse(idString, out var id))
            {
                var profissional = await _repository.FindByIdAsyc(id);

                if(profissional == null )
                    return new Error("Não existe um profissional com esse Id");

                if (!await _passwordService.CompareAsync(changePasswordCmd.Password, profissional.Password!, profissional.Salt!))
                    return new Error("senha inválida");

                var pwdHash = await _passwordService.HandleAsync(changePasswordCmd.NewPassword, out string salt, cancellationToken);

                profissional!.Password = pwdHash;
                profissional.Salt = salt;
                _repository.Update(profissional);
                await _unityOfWork.SaveChangesAsync(cancellationToken);
                return Result.Success;
            }
            return new Error("Token inválido");
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

    public async Task<Result<string>> ResetPasswordAsync(string profissionalId, CancellationToken cancellationToken)
    {
        try
        {
            if (Guid.TryParse(profissionalId, out var id))
            {
                var profissional = await _repository.FindByIdAsyc(id);

                var firstPassword = RandomString(10);
                var pwdHash = await _passwordService.HandleAsync(firstPassword, out string salt, cancellationToken);
                profissional!.Password= pwdHash;
                profissional.Salt = salt;
                _repository.Update(profissional);
                await _unityOfWork.SaveChangesAsync(cancellationToken);
                return firstPassword;
            }
            return new Error("Invalid Id");
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }
    public async Task<Result> RemoveAsync(string professionalId, CancellationToken cancellationToken)
    {
        try
        {
            if (Guid.TryParse(professionalId, out var id))
            {
                await _repository.DeleteByIdAsync(id, cancellationToken);
                await _unityOfWork.SaveChangesAsync(cancellationToken);
                return Result.Success;
            }
            return new Error("Invalid Id");
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

}
