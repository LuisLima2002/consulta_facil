
// internal imports
using VozAmiga.Core.DTO.ViewModels;
using VozAmiga.Core.DTO.Query;
using VozAmiga.Api.Utils;
using VozAmiga.Core.Services.Interface.Patient;
using VozAmiga.Core.Data.Repositories;
using VozAmiga.Core.Data.Model;
using VozAmiga.Api.Utils.Enums;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VozAmiga.Api.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace VozAmiga.Api.Services.V1;

public class QueryPatientService : IQueryPatientService
{
    private readonly ILogger<QueryPatientService> _logger;
    private readonly IPatientRepository _patientRepository;

    public QueryPatientService(
        ILogger<QueryPatientService> logger,
        IPatientRepository patientRepository
    )
    {
        _logger = logger;
        _patientRepository = patientRepository;
    }
    public async Task<Result<PatientQR?>> GetPatientAsync(string patientId, CancellationToken cancellationToken)
    {
        if (Guid.TryParse(patientId, out var id))
        {
            Patient? patient = await _patientRepository.FindByIdAsyc(id, cancellationToken);
            if (patient == null)
                return new ArgumentOutOfRangeException(nameof(patientId), "Não foi encontrado nenhum paciente com esse id");
            return ToQR(patient);
        }
        return new Error("Invalid Id");
    }

    public async Task<Result<Paginated<PatientQR>>> GetPatientsAsync(PersonQuery query, CancellationToken cancellationToken = default)
    {
        var found = _patientRepository.FindAsyc(query.filter ?? "", cancellationToken);

        if (query.orderBy != null)
        {
            switch (query.orderBy)
            {
                case "name":
                    found = found.OrderBy(person => person.Name.ToLower()); break;
                case "deathday":
                    found = found.OrderBy(person => person.DeathDay); break;
                case "birthdate":
                    found = found.OrderBy(person => person.Birthdate); break;
                default:
                    found = found.OrderBy(person => person.Name.ToLower()); break;
            }
        }

        var total = await found.CountAsync(cancellationToken: cancellationToken);

        IEnumerable<PatientQR> res = found
            .Skip(query.page * query.itensPerpage)
            .Take(query.itensPerpage)
            .Select(ToQR);

        var result = new Paginated<PatientQR>(res)
        {
            Page = query.page,
            ItemsPerPage = query.itensPerpage,
            Total = total,
        };

        return result;
    }

    private static PatientQR ToQR(Patient patient)
    {
        var res = new PatientQR
        {
            Id = patient.Id.ToString(),
            Name = patient.Name,
            Birthdate = patient.Birthdate,
            Document = patient.Document,
            Gender = patient.Gender,
            Address = patient.Address,
            PhoneNumber = patient.PhoneNumber,
            HealthInsurance = patient.HealthInsurance,
            DeathDay = patient.DeathDay,
            DeathReason = patient.DeathReason,
        };

        return res;
    }

}
