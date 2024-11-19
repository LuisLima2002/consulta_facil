

using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using VozAmiga.Api.Data.Database;
using VozAmiga.Core.Data.Model;
using VozAmiga.Core.Data.Repositories;
using VozAmiga.Core.DTO.Commands;
using VozAmiga.Core.DTO.ViewModels;
using VozAmiga.Api.Infra.Repositories;
using VozAmiga.Core.Services.Interface.Patient;
using VozAmiga.Api.Utils;

namespace VozAmiga.Api.Services.V1;

public class CreatePatientService : ICreatePatientService
{
    private readonly IPatientRepository _repository;
    private readonly ILogger<CreatePatientService> _logger;
    private readonly IValidator<CreatePatientCmd> _validator;
    private readonly IUnityOfWork _unityOfWork;

    public CreatePatientService(
        IPatientRepository repository,
        ILogger<CreatePatientService> logger,
        IValidator<CreatePatientCmd> validator,
        IUnityOfWork unityOfWork
    )
    {

        _repository = repository;
        _logger = logger;
        _validator = validator;
        _unityOfWork = unityOfWork;
    }

    public async Task<Result> UpdateAsync(PatientQR patientQR, CancellationToken cancellationToken)
    {
        try
        {
            if (Guid.TryParse(patientQR.Id, out var id))
            {
                Patient patient = new Patient(id) { Name = patientQR.Name,
                    Birthdate = patientQR.Birthdate,
                    Document = patientQR.Document,
                    Gender = patientQR.Gender,
                    Address = patientQR.Address,
                    PhoneNumber = patientQR.PhoneNumber,
                    HealthInsurance = patientQR.HealthInsurance,
                    DeathDay = patientQR.DeathDay,
                    DeathReason = patientQR.DeathReason,
                };
                _repository.Update(patient);
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

    public async Task<Result> RemoveAsync(string patientId, CancellationToken cancellationToken)
    {
        try
        {
            if (Guid.TryParse(patientId, out var id))
            {
                await _repository.DeleteByIdAsync(id, cancellationToken);
                await _unityOfWork.SaveChangesAsync(cancellationToken);
                return Result.Success;
            }
            return new Error("Invalid Id");
        }
        catch (Exception ex) {
            return new Error(ex.Message);
        }
    }

    /// <inheritdoc />
    public async Task<Result<string>> HandleAsync(CreatePatientCmd cmd, CancellationToken cancellationToken)
    {

        var validationResult = await _validator.ValidateAsync(cmd, cancellationToken);
        if (!validationResult.IsValid)
            return new Error(validationResult.Errors.Select(x => x.ErrorMessage).ToArray());


        var patient = new Patient(Guid.NewGuid())
        {
            Name = cmd.Name,
            Birthdate = cmd.Birthdate,
            Document = cmd.Document,
            Gender = cmd.Gender,
            Address = cmd.Address,
            PhoneNumber = cmd.PhoneNumber,
            HealthInsurance = cmd.HealthInsurance,
        };

        await _repository.AddAsync(patient, cancellationToken);
        await _unityOfWork.SaveChangesAsync(cancellationToken);

        return patient.Id.ToString();
    }


}
