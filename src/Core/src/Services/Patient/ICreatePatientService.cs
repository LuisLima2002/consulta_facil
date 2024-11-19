

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VozAmiga.Api.Data.Database;
using VozAmiga.Core.Data.Model;
using VozAmiga.Core.Data.Repositories;
using VozAmiga.Core.DTO.Commands;
using VozAmiga.Core.DTO.ViewModels;
using VozAmiga.Api.Utils;
using VozAmiga.Api.Utils.Enums;

namespace VozAmiga.Core.Services.Interface.Patient;

public interface ICreatePatientService
{
    /// <summary>
    /// Creates a new Patient and returns its id
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Result<string>> HandleAsync(CreatePatientCmd cmd, CancellationToken cancellationToken = default);

    public Task<Result> RemoveAsync(string patientId, CancellationToken cancellationToken = default);

    public Task<Result> UpdateAsync(PatientQR patient, CancellationToken cancellationToken = default);

}
